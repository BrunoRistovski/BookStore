using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain_Models.MusicStoreModels
{
    public class Album
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public DateTime ReleaseDate { get; set; }

        public int Price { get; set; }
        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; }
    }
}
