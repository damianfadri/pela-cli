using System.Text;

namespace Pela.Core
{
    public class Solution
    {
        public Area Area { get; }
        public List<Assistant> Assistants { get; }

        public int TourDuration =>
            Assistants.Sum(assistant => assistant.TourDuration);

        public int EducationalValue =>
            Assistants.Sum(assistant => assistant.EducationalValue);

        public int VisitorAppeal =>
            Assistants.Sum(assistant => assistant.VisitorAppeal);

        public double Waste =>
            (
                Math.Pow(Math.Max(0, Area.TourDuration - TourDuration), 2)
                + Math.Pow(Math.Max(0, Area.EducationalValue - EducationalValue), 2)
                + Math.Pow(Math.Max(0, Area.VisitorAppeal - VisitorAppeal), 2)
            )
            / Assistants.Count;

        public bool IsSolved =>
            Area.TourDuration <= TourDuration
            && Area.EducationalValue <= EducationalValue
            && Area.VisitorAppeal <= VisitorAppeal;

        public bool IsFullyAppointed => Assistants.Count == 3;


        public Solution(Area area)
        {
            Assistants = new List<Assistant>();
            Area = area;
        }

        public Solution(Area area, List<Assistant> assistants)
        {
            Assistants = assistants;
            Area = area;
        }

        public Solution Clone()
        {
            var assistants = new List<Assistant>();
            foreach (var assistant in Assistants)
            {
                assistants.Add(assistant.Clone());
            }

            return new Solution(
                Area.Clone(),
                assistants);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine(Area.Name);
            foreach (var assistant in Assistants)
            {
                sb.AppendLine($"  {assistant.Name}");
            }

            if (!IsSolved)
            {
                sb.AppendLine();
                sb.AppendLine("  Modifications:");
                if (TourDuration < Area.TourDuration)
                {
                    sb.AppendLine($"    Tour Duration: +{Area.TourDuration - TourDuration}");
                }

                if (EducationalValue < Area.EducationalValue)
                {
                    sb.AppendLine($"    Educational Value: +{Area.EducationalValue - EducationalValue}");
                }

                if (VisitorAppeal < Area.VisitorAppeal)
                {
                    sb.AppendLine($"    Visitor Appeal: +{Area.VisitorAppeal - VisitorAppeal}");
                }
            }

            return sb.ToString();
        }
    }
}