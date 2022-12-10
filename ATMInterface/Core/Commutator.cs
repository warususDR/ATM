using System.Collections.Generic;

namespace ATM
{
    public class eCommutator//one for all
    {
        private List<Node> nodes;//list of existing nodes to send payload to

        public eCommutator()
        {
            nodes = new List<Node>();
        }

        public void Send(eLog _payload) { nodes.Find(i => i.Name == _payload.Header.dst).Receive(_payload); }//sending payload to a dst from header

        public void Register(Node node) { nodes.Add(node); }//nodes register themselves in commutator
    }

    public abstract class Node
    {

        protected eCommutator Owner { get; set; }//node owner where nodes register themselves
        protected void Init() { if (Owner != null)  Owner.Register(this); }//register node in commutator
        protected void Send(eLog _payload) { if (Owner != null) Owner.Send(_payload); }//send payload by commutator
        
        public Node(string name, eCommutator commutator) 
        {
            Name = name;
            Owner = commutator;
            ReqSenders = new Stack<string>(); 
        }
        public string Name { get; set; }//node name to find it by among others
        public Stack<string> ReqSenders { get; set; }//remember where to send an ack for req
        public void Receive(eLog payload) { Process(payload); }//override what you gonna do with received payload
        protected abstract void Process(eLog payload);
    }
}
