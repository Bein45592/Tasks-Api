using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using Task = TasksApi.Model.Task;

namespace TasksApi.Services
{
    public class TaskService
    {
        private readonly string tasksFilePath = Path.Combine("Data", "Tasks.json");
        private List<Task> tasks;

        public TaskService()
        {
            LoadTasks();
        }

        private void LoadTasks()
        {
            if (File.Exists(tasksFilePath))
            {
                var json = File.ReadAllText(tasksFilePath);
                tasks = JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
            }
            else
            {
                tasks = new List<Task>();
            }
        }

        private void SaveTasks()
        {
            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(tasksFilePath, json);
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return tasks;
        }

        public Task GetTaskById(int id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public void AddTask(Task newTask)
        {
            newTask.Id = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;
            tasks.Add(newTask);
            SaveTasks();
        }

        public void UpdateTask(int id, Task updatedTask)
        {
            var taskIndex = tasks.FindIndex(t => t.Id == id);
            if (taskIndex != -1)
            {
                updatedTask.Id = id;
                tasks[taskIndex] = updatedTask;
                SaveTasks();
            }
            else
            {
                throw new KeyNotFoundException("Task not found.");
            }
        }

        public void DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                SaveTasks();
            }
            else
            {
                throw new KeyNotFoundException("Task not found.");
            }
        }

    }
}