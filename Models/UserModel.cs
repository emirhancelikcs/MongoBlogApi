﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoBlogApi.Models;

public class UserModel
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }
	public string Name { get; set; }
	public string LastName { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }

}
