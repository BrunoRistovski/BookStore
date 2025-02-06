using Domain.Domain_Models;
using Domain.Domain_Models.MusicStoreModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class ArtistService : IArtistService
    {
        private readonly string _connectionString;

        public ArtistService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MusicStoreConnection");
        }

        public List<Artist> GetArtists()
        {
            var artists = new List<Artist>();
            string sql = "SELECT Id, Name, Bio FROM Artists";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var artist = new Artist
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Bio = reader["Bio"].ToString()
                            };
                            artists.Add(artist);
                        }
                    }
                }
            }
            return artists;
        }

        public List<Album> GetAlbums()
        {
            var albums = new List<Album>();

            string sql = @"
                SELECT a.Id, a.Title, a.ReleaseDate, a.ArtistId, a.Price, b.Name AS ArtistName
                FROM Albums a
                INNER JOIN Artists b ON a.ArtistId = b.Id
                ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var album = new Album
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                Title = reader["Title"].ToString(),
                                ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]),
                                ArtistId = Guid.Parse(reader["ArtistId"].ToString()),
                                Price = Convert.ToInt32(reader["Price"]),
                                ArtistName = reader["ArtistName"].ToString()
                            };
                            albums.Add(album);
                        }
                    }
                }
            }

            return albums;
        }
    }
    }

