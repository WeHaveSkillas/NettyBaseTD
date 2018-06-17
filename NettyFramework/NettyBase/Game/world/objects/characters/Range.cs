﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NettyBase.Game.world.objects.map;
using Object = NettyBase.Game.world.objects.map.Object;

namespace NettyBase.Game.world.objects.characters
{
    class Range
    {
        public ConcurrentDictionary<int, Character> Entities = new ConcurrentDictionary<int, Character>();
        public ConcurrentDictionary<int, Object> Objects = new ConcurrentDictionary<int, Object>();

        public Dictionary<string, Collectable> Collectables = new Dictionary<string, Collectable>();
        public Dictionary<string, Ore> Resources = new Dictionary<string, Ore>();
        public Dictionary<int, Zone> Zones = new Dictionary<int, Zone>();
       
        public Character Character { get; set; }

        public Character GetEntity(int id)
        {
            return Entities.ContainsKey(id) ? Entities[id] : null;
        }

        public event EventHandler<CharacterArgs> EntityAdded;
        public bool AddEntity(Character entity)
        {
            var success = Entities.TryAdd(entity.Id, entity);
            if (success) EntityAdded?.Invoke(this, new CharacterArgs(entity));
            return success;
        }

        public event EventHandler<CharacterArgs> EntityRemoved;
        public bool RemoveEntity(Character entity)
        {
            var success = Entities.TryRemove(entity.Id, out entity);
            if (success)
            {
                EntityRemoved?.Invoke(this, new CharacterArgs(entity));
            }
            return success;
        }

        public bool AddObject(Object obj)
        {
            var collectable = obj as Collectable;
            if (collectable != null)
            {
                Collectables.Add(collectable.Hash, collectable);
            }
            return Objects.TryAdd(obj.Id, obj);
        }

        public bool RemoveObject(Object obj)
        {
            var collectable = obj as Collectable;
            if (collectable != null)
            {
                Collectables.Remove(collectable.Hash);
            }
            return Objects.TryRemove(obj.Id, out obj);
        }

        public void Clear()
        {
            ClearEntities();
            Collectables.Clear();
            Resources.Clear();
            Zones.Clear();
            Objects.Clear();
        }

        private void ClearEntities()
        {
            foreach (var entity in Entities)
                EntityRemoved?.Invoke(this, new CharacterArgs(entity.Value));
            Entities.Clear();
        }
    }
}
