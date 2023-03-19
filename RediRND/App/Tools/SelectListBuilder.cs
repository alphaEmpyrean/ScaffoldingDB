using Microsoft.AspNetCore.Mvc.Rendering;
using RediRND.App.Entities;
using RediRND.App.Entities.ContainerAggregate;

namespace RediRND.App.Tools
{
    public class SelectListBuilder
    {
        public SelectListBuilder() { }

        public static List<SelectListItem> AllContainers(List<Container> baseList)
        {
            List<SelectListItem> selectList = new() { new SelectListItem { Value = "", Text = "None" } };
            foreach (var container in baseList)
            {
                selectList.Add(new SelectListItem { Value = container.Id.ToString(), Text = container.Name });
            }

            return selectList;
        }

        public static List<SelectListItem> AllStakers(List<Staker> baseList)
        {
            List<SelectListItem> selectList = new();
            // Build select list
            foreach (var staker in baseList)
            {
                selectList.Add(new SelectListItem { Value = staker.Id.ToString(), Text = $"{staker.LastName}, {staker.FirstName}" });
            }

            return selectList;
        }

        public static List<SelectListItem> ContainersNotAboveInHeirarchyPath(List<Container> baseList, int currentContainerId)
        {

            // Remove containers that are directly above in path to root
            Container? currentContainer = baseList.Find(c => c.Id == currentContainerId);
            while (currentContainer != null)
            {
                baseList.Remove(currentContainer);
                currentContainer = currentContainer.Parent;
            }

            List<SelectListItem> selectList = new();

            // Build select list
            foreach (var selectContainer in baseList)
            {
                selectList.Add(new SelectListItem { Value = selectContainer.Id.ToString(), Text = selectContainer.Name });
            }

            return selectList;
        }

        public static List<SelectListItem> ContainersNotBelowInHeirarchyPath(List<Container> baseList, int currentContainerId)
        {
            // Remove containers that are in the heirarchy path
            Container? currentContainer = baseList.Find(c => c.Id == currentContainerId);

            if (currentContainer != null)
            {
                Queue<Container> removalList = new();
                removalList.Enqueue(currentContainer);

                while (removalList.Count != 0)
                {
                    // Get next container to be removed and remove it
                    currentContainer = removalList.Dequeue();
                    baseList.Remove(currentContainer);                    

                    // Add all children to removal list
                    baseList.Where(c => c.ParentId == currentContainer.Id).ToList().ForEach(c => removalList.Enqueue(c));                  
                }
            }

            // Build Select list from remaining containers
            List<SelectListItem> selectList = new() { new SelectListItem { Value = "", Text = "None" } };
            foreach (var selectContainer in baseList)
            {
                selectList.Add(new SelectListItem { Value = selectContainer.Id.ToString(), Text = selectContainer.Name });
            }

            return selectList;
        }
    }
}
