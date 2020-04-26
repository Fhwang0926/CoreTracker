using System;

namespace CoreTracker
{
    class scoreBox
    {
        public void addCpuScore(int score)
        {
            cpu_temperature = cpu_temperature != 0 ? Convert.ToUInt16((cpu_temperature + score) / 2) : Convert.ToUInt16(score);
        }
        public void addRamScore(int score)
        {
            ram_usage = cpu_temperature != 0 ? Convert.ToUInt16((ram_usage + score) / 2) : Convert.ToUInt16(score);
            ram_usage = Convert.ToUInt16((ram_usage + score) / 2);
        }
        public void addboardScore(int score)
        {
            board_temperature = board_temperature != 0 ? Convert.ToUInt16((board_temperature + score) / 2) : Convert.ToUInt16(score);
        }
        public void addGpuScore(int score)
        {
            gpu_temperature = gpu_temperature != 0 ? Convert.ToUInt16((gpu_temperature + score) / 2) : Convert.ToUInt16(score);
        }
        public UInt16 cpu_temperature { get; set; } = 0;
        public UInt16 ram_usage { get; set; } = 0;
        public UInt16 board_temperature { get; set; } = 0;
        public UInt16 gpu_temperature { get; set; } = 0;
    }
    class github_result
    {
        public string tag_name { get; set; }
        public string target { get; set; }
        public string body { get; set; }
        public bool is_error { get; set; } = false;
    }
    class github
    {
        public string url { get; set; }
        public string assets_url { get; set; }
        public string upload_url { get; set; }
        public string html_url { get; set; }
        public Int64 id { get; set; }
        public string node_id { get; set; }
        public string tag_name { get; set; }
        public string target_commitish { get; set; }
        public string name { get; set; }
        public bool draft { get; set; }
        public assets[] assets { get; set; }
        public bool prerelease { get; set; }
        public DateTime created_at { get; set; }
        public DateTime published_at { get; set; }
        public string tarball_url { get; set; }
        public string zipball_url { get; set; }
        public string body { get; set; }
        public author author { get; set; }
    }

    class author
    {
        public string login { get; set; }
        public Int64 id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        
    }

    class assets
    {
        public uploader uploader { get; set; }
        public string url { get; set; }
        public Int64 id { get; set; }
        public string node_id { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public string content_type { get; set; }
        public string state { get; set; }
        public Int64 size { get; set; }
        public string download_count { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string browser_download_url { get; set; }
        

    }

    class uploader
    {
        public string login { get; set; }
        public Int64 id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    #region
    class updateFormat
    {
        public string target { get; set; }
        public string msg { get; set; }
        public bool is_error { get; set; } = false;
        public bool latest { get; set; } = false;
    }
    #endregion
}
