﻿namespace BlogMangementApi.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string BlogText { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
    }

}
