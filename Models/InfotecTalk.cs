using System;
using System.Collections.Generic;

namespace Infotec5Hub
{
    public class TalkHistory
    {
        public int id { get; set; }
        public int talk_id { get; set; }
        public int? user_id { get; set; }
        public int? queue_id { get; set; }
        public int? channel_id { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public bool has_attachment { get; set; }
        public object attachment_url { get; set; }
        public object attachment_type_id { get; set; }
        public string sent_at { get; set; }
        public object delivered_at { get; set; }
        public string read_at { get; set; }
        public bool? was_read { get; set; }
        public bool? has_highlight { get; set; }
        public string from { get; set; }
        public string external_id { get; set; }
        public object left_at { get; set; }
        public bool? has_error { get; set; }
        public object campaign_id { get; set; }
        public bool? was_blocked { get; set; }
        public bool? is_template { get; set; }
        public object response_from { get; set; }
        public string fare { get; set; }
        public string created_at { get; set; }
        public string to { get; set; }
    }

    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string external_id { get; set; }
        public string document_number { get; set; }
        public string wildcard_01 { get; set; }
        public string wildcard_02 { get; set; }
        public string wildcard_03 { get; set; }
        public string wildcard_04 { get; set; }
        public string wildcard_05 { get; set; }
        public string wildcard_06 { get; set; }
        public string wildcard_07 { get; set; }
        public string wildcard_08 { get; set; }
        public string wildcard_09 { get; set; }
        public string wildcard_10 { get; set; }
        public int? main_sms { get; set; }
        public int? main_whatsapp { get; set; }
        public int? main_email { get; set; }
        public int? agent_id { get; set; }
        public int? queue_id { get; set; }
        public object customer_group_id { get; set; }
        public object profile_picture_url { get; set; }
    }

    public class Event
    {
        public int id { get; set; }
        public string description { get; set; }
    }

    public class TalkEvent
    {
        public int id { get; set; }
        public int? talk_id { get; set; }
        public int? event_id { get; set; }
        public int? queue_id { get; set; }
        public bool? is_finisher { get; set; }
        public bool? is_success { get; set; }
        public int? agent_id { get; set; }
        public Event @event { get; set; }
    }    
    public class InfotecTalk
    {
        public int id { get; set; }
        public int? customer_id { get; set; }
        public int? agent_id { get; set; }
        public int? queue_id { get; set; }
        public int? channel_id { get; set; }
        public int? priority_id { get; set; }
        public string finished_at { get; set; }
        public bool? was_success { get; set; }
        public string created_at { get; set; }
        public bool? has_schedule { get; set; }
        public bool? is_critical { get; set; }
        public int? rating_survey_id { get; set; }
        public string tracking_number { get; set; }
        public List<TalkHistory> talk_histories { get; set; }
        public Customer customer { get; set; }
        public List<TalkEvent> talk_events { get; set; }
    }
}
