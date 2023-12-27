using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoBlogApi.Models;

public class BlogModel
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }
	public string Header { get; set; }
	public string Title { get; set; }
	public string Description { get; set; }
	public string CreatedTime {  get; set; }
	public UserModel User { get; set; }
}
