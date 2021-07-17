using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pohon.Models
{
    [Table("github_oauth_details")]
    public class GithubOAuthDetail
    {
        [Key, Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("login")]
        public string Login { get; set; }
        [Column("node_id")]
        public string NodeId { get; set; }

        public User User { get; set; }
        
        [Column("last_updated"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; set; }
    }
}