namespace Pela.Core
{
    public class Assistant
    {
        public string Name { get; }
        public int TourDuration { get; }
        public int EducationalValue { get; }
        public int VisitorAppeal { get; }

        public Assistant(
            string name,
            int tourDuration,
            int educationalValue,
            int visitorAppeal)
        {
            Name = name;
            TourDuration = tourDuration;
            EducationalValue = educationalValue;
            VisitorAppeal = visitorAppeal;
        }

        public Assistant Clone()
        {
            return new Assistant(
                Name,
                TourDuration,
                EducationalValue,
                VisitorAppeal);
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Assistant assistant)
            {
                return false;
            }

            return Name.Equals(assistant.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
