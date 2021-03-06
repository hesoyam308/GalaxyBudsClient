﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Galaxy_Buds_Client.message;
using Galaxy_Buds_Client.model;

namespace Galaxy_Buds_Client.parser
{
    class GenericResponseParser : BaseMessageParser
    {
        public override SPPMessage.MessageIds HandledType => SPPMessage.MessageIds.MSG_ID_RESP;
        public SPPMessage.MessageIds MessageId { set; get; }
        public int ResultCode { set; get; }
        public int? ExtraData { set; get; }

        public override void ParseMessage(SPPMessage msg)
        {
            if (msg.Id != HandledType)
                return;

            MessageId = (SPPMessage.MessageIds) msg.Payload[0];
            ResultCode = msg.Payload[1];
            if (msg.Payload.Length > 2)
            {
                ExtraData = msg.Payload[2];
            }
        }

        public override Dictionary<String, String> ToStringMap()
        {
            Dictionary<String, String> map = new Dictionary<string, string>();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "HandledType")
                    continue;

                map.Add(property.Name, property.GetValue(this)?.ToString());
            }

            return map;
        }
    }
}