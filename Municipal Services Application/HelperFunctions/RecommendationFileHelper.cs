using Municipal_Services_Application.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = System.Xml.Formatting;

namespace Municipal_Services_Application.HelperFunctions
{
    internal class RecommendationFileHelper
    {
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "storage", "RecommendedEvents.txt");

        // Static constructor to ensure the Storage folder is created when the class is first used
        static RecommendationFileHelper()
        {
            string storageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Storage");
            if (!Directory.Exists(storageFolder))
            {
                Directory.CreateDirectory(storageFolder);
            }
        }

        /// <summary>
        /// Saves the recommended events to a text file in JSON format.
        /// </summary>
        /// <param name="recommendedEvents">List of recommended events to save.</param>
        public static void SaveRecommendedEventsToFile(List<RecommendedEventsModel> recommendedEvents)
        {
            var json = JsonConvert.SerializeObject(recommendedEvents, (Newtonsoft.Json.Formatting)Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Loads the recommended events from the text file.
        /// </summary>
        /// <returns>List of recommended events.</returns>
        public static List<RecommendedEventsModel> LoadRecommendedEventsFromFile()
        {
            if (!File.Exists(filePath))
                return new List<RecommendedEventsModel>();

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<RecommendedEventsModel>>(json) ?? new List<RecommendedEventsModel>();
        }
    }
}
