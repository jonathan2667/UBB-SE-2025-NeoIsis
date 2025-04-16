using System;
using Windows.UI;

namespace NeoIsisJob.Models
{
    public class RankDefinition
    {
        public string Name { get; set; }
        public int MinPoints { get; set; }
        public int MaxPoints { get; set; }
        public Color Color { get; set; }
        public string ImagePath { get; set; }
    }
} 