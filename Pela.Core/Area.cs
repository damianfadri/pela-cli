namespace Pela.Core
{
    public class Area
    {
        public string Name { get; }
        public int TourDuration { get; }
        public int EducationalValue { get; }
        public int VisitorAppeal { get; }
        public int Priority { get; }
        public int Value => TourDuration + EducationalValue + VisitorAppeal;

        public Area(
            string name,
            int tourDuration,
            int educationalValue,
            int visitorAppeal,
            int priority = 0)
        {
            Name = name;
            TourDuration = tourDuration;
            EducationalValue = educationalValue;
            VisitorAppeal = visitorAppeal;
            Priority = priority;
        }

        public Area Clone()
        {
            return new Area(
                Name,
                TourDuration,
                EducationalValue,
                VisitorAppeal,
                Priority);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Area area)
            {
                return false;
            }

            return Name.Equals(area.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
