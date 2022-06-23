using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ChatterAPI.Hubs
{
    public class FirebaseService
    {
        FirebaseService()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile("private_key.json")
                });
            }
        }

        
    }
}