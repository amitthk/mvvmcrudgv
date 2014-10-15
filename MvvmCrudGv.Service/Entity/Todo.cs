using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using ProtoBuf;

namespace MvvmCrudGv.Service.Entity
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/MvvmCrudGv.Service.Domain")]
    [Serializable]
    [ProtoContract]
    public class Todo
    {
        private Guid _Id;
        private string _Title;
        private string _Text;
        private DateTime _CreateDt;
        private DateTime _DueDt;
        private int _EstimatedPomodori;
        private int _CompletedPomodori;
        private string _AddedBy;

        [DataMember]
        [ProtoMember(1)]
        public Guid Id
        {
            get { return _Id; }
            set {
                if (!_Id.Equals(value))
                {
                    _Id = value;
                }
            }
        }


        [DataMember]
        [ProtoMember(2)]
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }



        [DataMember]
        [ProtoMember(3)]
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }


        [DataMember]
        [ProtoMember(4)]
        public DateTime CreateDt
        {
            get { return _CreateDt; }
            set { _CreateDt = value; }
        }


        [DataMember]
        [ProtoMember(5)]
        public DateTime DueDt
        {
            get { return _DueDt; }
            set { _DueDt = value; }
        }


        [DataMember]
        [ProtoMember(6)]
        public int EstimatedPomodori
        {
            get { return _EstimatedPomodori; }
            set { _EstimatedPomodori = value; }
        }


        [DataMember]
        [ProtoMember(7)]
        public int CompletedPomodori
        {
            get { return _CompletedPomodori; }
            set { _CompletedPomodori = value; }
        }




        [DataMember]
        [ProtoMember(8)]
        public string AddedBy
        {
            get { return _AddedBy; }
            set { _AddedBy = value; }
        }



        public Todo()
        {
            _Id = Guid.NewGuid();
            _CreateDt = DateTime.Now;
            _EstimatedPomodori = 1;
            _CompletedPomodori = 0;
            _DueDt = DateTime.Today.AddDays(1);
        }
    }
}
