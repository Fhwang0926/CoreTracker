using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreTracker
{
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
        public string msg { get; set; }
        public bool is_error { get; set; } = false;
        public bool latest { get; set; } = false;
    }
    #endregion
}
