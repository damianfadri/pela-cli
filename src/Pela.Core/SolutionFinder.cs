namespace Pela.Core
{
    public class SolutionFinder
    {
        private Area _area;
        private List<Assistant> _assistants;

        private List<Solution> _solutions;

        public SolutionFinder(Area area, List<Assistant> assistants)
        {
            _area = area;
            _assistants = assistants;
            _solutions = new List<Solution>();
        }

        public Solution FindSolution()
        {
            _solutions = new List<Solution>();

            SolveUtil(new Solution(_area), 0);

            return _solutions
                .OrderByDescending(solution => solution.IsSolved)
                .ThenBy(solution => solution.Waste)
                .FirstOrDefault(new Solution(_area));
        }

        private void SolveUtil(Solution solution, int assistantIndex)
        {
            if (assistantIndex == _assistants.Count)
            {
                return;
            }

            if (solution.IsFullyAppointed)
            {
                SolveUtil(solution, _assistants.Count);
                return;
            }

            var currentAssistant = _assistants[assistantIndex];

            var modifiedSolution = solution.Clone();
            modifiedSolution.Assistants.Add(currentAssistant);

            SolveUtil(modifiedSolution, assistantIndex + 1);
            _solutions.Add(modifiedSolution);

            SolveUtil(solution, assistantIndex + 1);
            _solutions.Add(solution);
        }
    }
}
