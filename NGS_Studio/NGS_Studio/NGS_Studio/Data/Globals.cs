using Firebase.Database;
using NGS_Studio.ViewModels;
using System;
using System.Threading.Tasks;
namespace NGS_Studio.Data
{
    public sealed class Globals : BaseViewModel
    {

        private string _token;

        public string AuthToken
        {
            get => _token;
            set => SetProperty(ref _token, value);
        }

        /// <summary>
        /// Connect app with firebase using API Url  
        /// </summary>
        private FirebaseClient _firebase = new FirebaseClient("https://ngs-studios-bd357-default-rtdb.firebaseio.com/",
        new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Globals.Instance.AuthToken)
        });

        private FirebaseClient _fireabaseInstane;

        public FirebaseClient Firebase
        {
            get => _fireabaseInstane;
            set => SetProperty(ref _fireabaseInstane, value);
        }
        private static readonly Lazy<Globals> lazy =
            new Lazy<Globals>(() => new Globals());

        public static Globals Instance { get { return lazy.Value; } }

        private Globals()
        {
            Firebase = _firebase;
        }
    }
}
