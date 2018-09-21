using System;
using VkNet.Model;

namespace ApiWrapper.Core
{
	public class PersonModel
	{
		internal PersonModel(User user)
		{
			this.id = user.Id;

			this.birthDate = user.BirthDate;

			this.city = user.City == null ? "" : user.City.Title;

			this.name = user.FirstName + " " + user.LastName;

			this.photoUrl400 = user.Photo400Orig;
			this.photoUrl200 = user.Photo200Orig;
			this.photoUrlMax = user.PhotoMaxOrig;
		    this.Status = user.Status;
		    this.interests = user.Interests;
		    this.followers = user.FollowersCount == null ? 0 : (int)user.FollowersCount;
		    this.Domain = user.Domain;
		    this.Relation = user.Relation.ToString();

		}

		public PersonModel(int userVkId) : this(SearchInstrument.GetUser(userVkId))
		{
		}


	    public string Relation { get; set; }

	    public string birthDate { get; set; }

		public Uri photoUrlMax { get; set; }

		public Uri photoUrl200 { get; set; }

		public Uri photoUrl400 { get; set; }

        public string Status { get; set; }
        public string interests { get; set; }

	    public int followers { get; set; }

        public string Domain { get; set; }

        public long id { get; set; }

		public string name { get; set; }

		public string city { get; set; }

	    public int statusCode
	    {
	        get
	        {
	            if (Relation == "InActiveSearch")
	            {
	                return 0;
	            }
	            if (Relation == "ItsComplex")
	            {
	                return 1;
	            }
	            if (Relation == "NotMarried")
	            {
	                return 2;
	            }
	            if (Relation == "HasFriend")
	            {
	                return 200;
	            }
	            if (Relation == "Engaged")
	            {
	                return 2000;
	            }
                return 100;

	        }
	    }



	}
}