﻿using dotSpace.BaseClasses;
using dotSpace.Enumerations;
using dotSpace.Interfaces;
using dotSpace.Objects.Network.Messages.Requests;
using dotSpace.Objects.Network.Messages.Responses;
using dotSpace.Objects.Network.Protocols;
using System;
using System.Linq;
using System.Collections.Generic;

namespace dotSpace.Objects.Network
{
    public sealed class ClientNode : NodeBase
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Fields

        private ConnectionMode mode;
        private Dictionary<ConnectionMode, ProtocolBase> protocols;

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Constructors

        public ClientNode(ConnectionMode mode, string address, int port) : base(address, port)
        {
            this.mode = mode;
            this.protocols = new Dictionary<ConnectionMode, ProtocolBase>();
            this.protocols.Add(ConnectionMode.CONN, new ConnProtocol(this));
            //this.protocols.Add(ConnectionMode.PUSH, new PushProtocol(this));
            //this.protocols.Add(ConnectionMode.PULL, new PushProtocol(this));
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Public Methods

        public ISpace GetRemoteSpace(string target)
        {
            return new SpaceWrapper(target, this);
        }
        public override ITuple Get(string target, IPattern pattern)
        {
            return this.Get(target, pattern.Fields);
        }
        public override ITuple Get(string target, params object[] pattern)
        {
            GetRequest request = new GetRequest(this.mode, this.GetSource(), this.GetSessionId(), target, pattern);
            GetResponse response = this.GetProtocol()?.PerformRequest<GetResponse>(this.CreateEndpoint(), request);
            return response.Result == null ? null : new Tuple(response.Result);
        }
        public override ITuple GetP(string target, IPattern pattern)
        {
            return this.GetP(target, pattern.Fields);
        }
        public override ITuple GetP(string target, params object[] pattern)
        {
            GetPRequest request = new GetPRequest(this.mode, this.GetSource(), this.GetSessionId(), target, pattern);
            GetPResponse response = this.GetProtocol()?.PerformRequest<GetPResponse>(this.CreateEndpoint(), request);
            return response.Result == null ? null : new Tuple(response.Result);
        }
        public override IEnumerable<ITuple> GetAll(string target, IPattern pattern)
        {
            return this.GetAll(target, pattern.Fields);
        }
        public override IEnumerable<ITuple> GetAll(string target, params object[] pattern)
        {
            GetAllRequest request = new GetAllRequest(this.mode, this.GetSource(), this.GetSessionId(), target, pattern);
            GetAllResponse response = this.GetProtocol()?.PerformRequest<GetAllResponse>(this.CreateEndpoint(), request);
            return response.Result == null ? null : response.Result.Select(x => new Tuple(x));
        }
        public override ITuple Query(string target, IPattern pattern)
        {
            return this.Query(target, pattern.Fields);
        }
        public override ITuple Query(string target, params object[] pattern)
        {
            QueryRequest request = new QueryRequest(this.mode, this.GetSource(), this.GetSessionId(), target, pattern);
            QueryResponse response = this.GetProtocol()?.PerformRequest<QueryResponse>(this.CreateEndpoint(), request);
            return response.Result == null ? null : new Tuple(response.Result);
        }
        public override ITuple QueryP(string target, IPattern pattern)
        {
            return this.QueryP(target, pattern.Fields);
        }
        public override ITuple QueryP(string target, params object[] pattern)
        {
            QueryPRequest request = new QueryPRequest(this.mode, this.GetSource(), this.GetSessionId(), target, pattern);
            QueryPResponse response = this.GetProtocol()?.PerformRequest<QueryPResponse>(this.CreateEndpoint(), request);
            return response.Result == null ? null : new Tuple(response.Result);
        }
        public override IEnumerable<ITuple> QueryAll(string target, IPattern pattern)
        {
            return this.QueryAll(target, pattern.Fields);
        }
        public override IEnumerable<ITuple> QueryAll(string target, params object[] pattern)
        {
            QueryAllRequest request = new QueryAllRequest(this.mode, this.GetSource(), this.GetSessionId(), target, pattern);
            QueryAllResponse response = this.GetProtocol()?.PerformRequest<QueryAllResponse>(this.CreateEndpoint(), request);
            return response.Result == null ? null : response.Result.Select(x => new Tuple(x));
        }
        public override void Put(string target, ITuple tuple)
        {
            this.Put(target, tuple.Fields);
        }
        public override void Put(string target, params object[] tuple)
        {
            PutRequest request = new PutRequest(this.mode, this.GetSource(), this.GetSessionId(), target, tuple);
            this.GetProtocol()?.PerformRequest<PutResponse>(this.CreateEndpoint(), request);
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Methods

        private string GetSessionId()
        {
            return Guid.NewGuid().ToString();
        }
        private string GetSource()
        {
            return this.GetLocalIPAddress();
        }
        private ProtocolBase GetProtocol()
        {
            if (this.protocols.ContainsKey(this.mode))
            {
                return this.protocols[this.mode];
            }

            return null; // TODO: return response error
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        #region // Private Classes

        private class SpaceWrapper : ISpace
        {
            private string target;
            private NodeBase node;

            public SpaceWrapper(string target, NodeBase node)
            {
                this.target = target;
                this.node = node;
            }
            public ITuple Get(params object[] values)
            {
                return this.node.Get(this.target, values);
            }

            public ITuple Get(IPattern pattern)
            {
                return this.node.Get(this.target, pattern);
            }

            public IEnumerable<ITuple> GetAll(params object[] values)
            {
                return this.node.GetAll(this.target, values);
            }

            public IEnumerable<ITuple> GetAll(IPattern pattern)
            {
                return this.node.GetAll(this.target, pattern);
            }

            public ITuple GetP(params object[] values)
            {
                return this.node.GetP(this.target, values);
            }

            public ITuple GetP(IPattern pattern)
            {
                return this.node.GetP(this.target, pattern);
            }

            public void Put(params object[] values)
            {
                this.node.Put(this.target, values);
            }

            public void Put(ITuple tuple)
            {
                this.node.Put(this.target, tuple);
            }

            public ITuple Query(params object[] values)
            {
                return this.node.Query(this.target, values); 
            }

            public ITuple Query(IPattern pattern)
            {
                return this.node.Query(this.target, pattern); 
            }

            public IEnumerable<ITuple> QueryAll(params object[] values)
            {
                return this.node.QueryAll(this.target, values); 
            }

            public IEnumerable<ITuple> QueryAll(IPattern pattern)
            {
                return this.node.QueryAll(this.target, pattern); 
            }

            public ITuple QueryP(params object[] values)
            {
                return this.node.QueryP(this.target, values); 
            }

            public ITuple QueryP(IPattern pattern)
            {
                return this.node.QueryP(this.target, pattern); 
            }
        }

        #endregion
    }
}
