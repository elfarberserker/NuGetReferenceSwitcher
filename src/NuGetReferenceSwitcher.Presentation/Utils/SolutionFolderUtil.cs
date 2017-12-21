using System.Collections;
using System.Collections.Generic;
using EnvDTE;
using EnvDTE80;
using NuGetReferenceSwitcher.Presentation.Models;

namespace NuGetReferenceSwitcher.Presentation.Utils
{
    public class SolutionFolderUtil
    {
        public const string vsProjectKindSolutionFolder = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";
        public static IList<SolutionFolderModel> GetSolutionFolders(DTE application)
        {
            var list = new List<SolutionFolderModel>();

            list.Add(new SolutionFolderModel()
            {
                Name = "Solution root",
                Path = "/"
            });

            var activeIDE = application as DTE2;
            if (activeIDE == null)
            {
                return list;
            }

            Projects projects = activeIDE.Solution.Projects;
            list.AddRange(GetFolders(projects, ""));

            return list;
        }

        private static IEnumerable<SolutionFolderModel> GetFolders(Projects projects, string parentFolderPath)
        {
            var list = new List<SolutionFolderModel>();
            var items = projects.GetEnumerator();
            while (items.MoveNext())
            {
                var project = items.Current as Project;
                if (project == null)
                {
                    continue;
                }

                if (project.Kind == vsProjectKindSolutionFolder)
                {
                    var name = project.Name;
                    var path = $"{parentFolderPath}/{name}";
                    list.Add(new SolutionFolderModel()
                    {
                        Name = name,
                        Path = path
                    });
                    list.AddRange(GetFolders(project, path));
                }
            }
            return list;
        }
        private static IEnumerable<SolutionFolderModel> GetFolders(Project parent, string parentFolderPath)
        {
            var list = new List<SolutionFolderModel>();
            for (var i = 1; i <= parent.ProjectItems.Count; i++)
            {
                var project = parent.ProjectItems.Item(i).SubProject as Project;
                if (project == null)
                {
                    continue;
                }

                if (project.Kind == vsProjectKindSolutionFolder)
                {
                    var name = project.Name;
                    var path = $"{parentFolderPath}/{name}";
                    list.Add(new SolutionFolderModel()
                    {
                        Name = name,
                        Path = path
                    });
                    list.AddRange(GetFolders(project, path));
                }
            }
            return list;
        }

        public static Project FindProjectBySolutionPath(DTE application, string path)
        {
            Project p = null;

            var activeIDE = application as DTE2;
            if (activeIDE == null)
            {
                return null;
            }

            Projects projects = activeIDE.Solution.Projects;

            return FindProjectBySolutionPath(projects, path, "");
        }
        private static Project FindProjectBySolutionPath(Projects projects, string path, string parentFolderPath)
        {
            Project p = null;

            var items = projects.GetEnumerator();
            while (items.MoveNext())
            {
                var project = items.Current as Project;
                if (project == null)
                {
                    continue;
                }

                if (project.Kind == vsProjectKindSolutionFolder)
                {
                    var name = project.Name;
                    var projectPath = $"{parentFolderPath}/{name}";
                    if (projectPath == path)
                    {
                        p = project;
                        break;
                    }

                    p = FindProjectBySolutionPath(project, path, projectPath);
                    if (p != null)
                    {
                        break;
                    }
                }
            }

            return p;
        }
        private static Project FindProjectBySolutionPath(Project parent, string path, string parentFolderPath)
        {
            Project p = null;
            for (var i = 1; i <= parent.ProjectItems.Count; i++)
            {
                var project = parent.ProjectItems.Item(i).SubProject as Project;
                if (project == null)
                {
                    continue;
                }

                if (project.Kind == vsProjectKindSolutionFolder)
                {
                    var name = project.Name;
                    var projectPath = $"{parentFolderPath}/{name}";
                    if (projectPath == path)
                    {
                        p = project;
                        break;
                    }

                    p = FindProjectBySolutionPath(project, path, projectPath);
                    if (p != null)
                    {
                        break;
                    }
                }
            }

            return p;
        }
    }
}
