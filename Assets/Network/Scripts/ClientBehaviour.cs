using Fusion;

namespace Network
{
    public class ClientBehaviour : NetworkBehaviour
    {
        public override void Spawned()
        {
            if (Runner.IsClient) return;
            enabled = false;
            base.Spawned();
        }
    }
}