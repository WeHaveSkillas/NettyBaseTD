﻿using System.Collections.Generic;
using NettyBase.Game.world.objects.map.pois;

namespace NettyBase.Game.world.objects.map
{
    class POI
    {
        /// <summary>
        /// POI Id
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Type of POI
        /// </summary>
        public Types Type { get; set; }

        /// <summary>
        /// Design of POI
        /// </summary>
        public Designs Design { get; set; }

        /// <summary>
        /// Shape of POI
        /// </summary>
        public Shapes Shape { get; set; }

        public List<Vector> ShapeCords { get; set; }

        /// <summary>
        /// If POI is inverted
        /// </summary>
        public bool Inverted { get; set; }

        /// <summary>
        /// More details for POI
        /// </summary>
        public string TypeSpecification { get; set; }

        public bool Active { get; set; }

        public POI(string id, Types type, Designs design, Shapes shape, List<Vector> shapeCords, bool active = true, bool inverted = false, string poiTypeSpecification = "")
        {
            Id = id;
            Type = type;
            Design = design;
            Shape = shape;
            ShapeCords = shapeCords;
            Inverted = inverted;
            TypeSpecification = poiTypeSpecification;
            Active = active;
        }

        public List<int> ShapeCordsToInts()
        {
            List<int> cords = new List<int>();
            foreach (var cord in ShapeCords)
            {
                cords.Add(cord.X);
                cords.Add(cord.Y);
            }
            return cords;
        }
    }
}