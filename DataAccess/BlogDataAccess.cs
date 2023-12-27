using MongoBlogApi.Models;
using MongoDB.Driver;

namespace MongoBlogApi.DataAccess;

public class BlogDataAccess
{
	private const string ConnectionString = "your mongodb connection!";
	private const string DatabaseName = "BlogWebsite";
	private const string UsersCollection = "users";//table in sql
	private const string BlogsCollection = "blogs";//table in sql

	private static IMongoCollection<T> ConnectToMongo<T>(in string collection)
	{
		MongoClient client = new MongoClient(ConnectionString);
		IMongoDatabase db = client.GetDatabase(DatabaseName);

		return db.GetCollection<T>(collection);
	}

	public async Task<List<UserModel>> GetAllUsers()
	{
		IMongoCollection<UserModel> userCollection = ConnectToMongo<UserModel>(UsersCollection);
		IAsyncCursor<UserModel> results = await userCollection.FindAsync(_ => true);

		return results.ToList();
	}

	public async Task<UserModel> GetUserById(string id)
	{
		IMongoCollection<UserModel> userCollection = ConnectToMongo<UserModel>(UsersCollection);
		IAsyncCursor<UserModel> user = await userCollection.FindAsync(u => u.Id == id);

		return user.FirstOrDefault();
	}

	public async Task<UserModel> GetUserByEmail(string email)
	{
		IMongoCollection<UserModel> userCollection = ConnectToMongo<UserModel>(UsersCollection);
		IAsyncCursor<UserModel> user = await userCollection.FindAsync(u => u.Email == email);

		return user.FirstOrDefault();
	}

	public async Task<UserModel> GetUserByEmailAndPassword(string email, string password)
	{
		IMongoCollection<UserModel> userCollection = ConnectToMongo<UserModel>(UsersCollection);
		IAsyncCursor<UserModel> user = await userCollection.FindAsync(u => u.Email == email && u.Password == password);

		return user.FirstOrDefault();
	}

	public Task CreateUser(UserModel user)
	{
		IMongoCollection<UserModel> userCollection = ConnectToMongo<UserModel>(UsersCollection);

		return userCollection.InsertOneAsync(user);
	}

	public Task UpdateUser(UserModel user)
	{
		IMongoCollection<UserModel> usersCollection = ConnectToMongo<UserModel>(UsersCollection);
		FilterDefinition<UserModel> filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
		return usersCollection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
		//IsUpsert means look for Replace/Update, and if there is no recored, insert new one!
	}

	public Task DeleteUser(string Id)
	{
		IMongoCollection<UserModel> userCollection = ConnectToMongo<UserModel>(UsersCollection);

		return userCollection.DeleteOneAsync(u => u.Id == Id);
	}

	public async Task<List<BlogModel>> GetBlogs()
	{
		IMongoCollection<BlogModel> blogCollection = ConnectToMongo<BlogModel>(BlogsCollection);
		IAsyncCursor<BlogModel> results = await blogCollection.FindAsync(_ => true);

		return results.ToList();
	}

	public async Task<BlogModel> GetBlogById(string id)
	{
		IMongoCollection<BlogModel> blogCollection = ConnectToMongo<BlogModel>(BlogsCollection);
		IAsyncCursor<BlogModel> blog = await blogCollection.FindAsync(b => b.Id == id);

		return blog.FirstOrDefault();
	}

	public async Task<List<BlogModel>> GetBlogByUserEmail(string email)
	{
		IMongoCollection<BlogModel> blogCollection = ConnectToMongo<BlogModel>(BlogsCollection);
		IAsyncCursor<BlogModel> blogs = await blogCollection.FindAsync(b => b.User.Email == email);

		return blogs.ToList();
	}

	public Task CreateBlog(BlogModel blog)
	{
		IMongoCollection<BlogModel> blogCollection = ConnectToMongo<BlogModel>(BlogsCollection);

		return blogCollection.InsertOneAsync(blog);
	}

	public Task DeleteBlog(string Id)
	{
		IMongoCollection<BlogModel> blogCollection = ConnectToMongo<BlogModel>(BlogsCollection);

		return blogCollection.DeleteOneAsync(b => b.Id == Id);
	}
}
