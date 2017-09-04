namespace LiveHAPI.Core.Model
{
   public class AppDashboard
    {
        public Module DefaultModule { get; set; }
        public User SignedInUser { get; set; }
        public Device Device { get; set; }

        public AppDashboard(Module defaultModule, User signedInUser,Device device)
        {
            DefaultModule = defaultModule;
            SignedInUser = signedInUser;
            Device = device;
        }

        public override string ToString()
        {
            return $"{DefaultModule} {SignedInUser} {Device}";
        }
    }
}
