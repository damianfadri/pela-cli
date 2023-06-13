namespace Pela.Core
{
    public class Solver
    {
        private IEnumerable<Area> _areas;
        private IEnumerable<Assistant> _assistants;

        public Solver(IEnumerable<Area> areas, IEnumerable<Assistant> assistants)
        {
            _areas = areas;
            _assistants = assistants;
        }

        public List<Solution> Solve()
        {
            var assistants = _assistants.ToList();
            var solutionMap = new Dictionary<string, Solution>();

            foreach (var area in _areas.OrderByDescending(area => area.Value))
            {
                var finder = new SolutionFinder(area, assistants);
                var solution = finder.FindMostOptimalSolution();

                solutionMap.Add(area.Name, solution);

                foreach (var assistant in solution.Assistants)
                {
                    assistants.Remove(assistant);
                }
            }

            var solutions = new List<Solution>();
            foreach (var area in _areas)
            {
                solutions.Add(solutionMap[area.Name]);
            }

            return solutions;
        }
    }
}
