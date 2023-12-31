﻿// Classes and structures being serialized

// Generated by ProtocolBuffer
// - a pure c# code generation implementation of protocol buffers
// Report bugs to: https://silentorbit.com/protobuf/

// DO NOT EDIT
// This file will be overwritten when CodeGenerator is run.
// To make custom modifications, edit the .proto file and add //:external before the message line
// then write the code and the changes in a separate file.
using System;
using System.Collections.Generic;

namespace TransitRealtime
{
    /// <summary>
    /// <para> Copyright 2011 Google Inc</para>
    /// <para></para>
    /// <para> The content of this file is licensed under the Creative Commons Attribution</para>
    /// <para> 3.0 License.</para>
    /// <para></para>
    /// <para> Protocol definition file for GTFS-realtime.</para>
    /// <para></para>
    /// <para> GTFS-realtime lets transit agencies provide consumers with realtime</para>
    /// <para> information about disruptions to their service (stations closed, lines not</para>
    /// <para> operating, important delays etc), location of their vehicles and expected</para>
    /// <para> arrival times.</para>
    /// <para></para>
    /// <para> This protocol is published at:</para>
    /// <para> https://developers.google.com/transit/gtfs-realtime/</para>
    /// <para> The contents of a feed message.</para>
    /// <para> A feed is a continuous stream of feed messages. Each message in the stream is</para>
    /// <para> obtained as a response to an appropriate HTTP GET request.</para>
    /// <para> A realtime feed is always defined with relation to an existing GTFS feed.</para>
    /// <para> All the entity ids are resolved with respect to the GTFS feed.</para>
    /// <para></para>
    /// <para> A feed depends on some external configuration:</para>
    /// <para> - The corresponding GTFS feed.</para>
    /// <para> - Feed application (updates, positions or alerts). A feed should contain only</para>
    /// <para>   items of one specified application; all the other entities will be ignored.</para>
    /// <para> - Polling frequency</para>
    /// </summary>
    public partial class FeedMessage
    {
        /// <summary> Metadata about this feed and feed message.</summary>
        public TransitRealtime.FeedHeader header { get; set; }

        /// <summary> Contents of the feed.</summary>
        public List<TransitRealtime.FeedEntity> entity { get; set; }

    }

    /// <summary> Metadata about a feed, included in feed messages.</summary>
    public partial class FeedHeader
    {
        public enum Incrementality
        {
            FULL_DATASET = 0,
            DIFFERENTIAL = 1,
        }

        /// <summary>
        /// <para> Version of the feed specification.</para>
        /// <para> The current version is 1.0.</para>
        /// </summary>
        public string gtfs_realtime_version { get; set; }

        public TransitRealtime.FeedHeader.Incrementality incrementality { get; set; }

        /// <summary>
        /// <para> This timestamp identifies the moment when the content of this feed has been</para>
        /// <para> created (in server time). In POSIX time (i.e., number of seconds since</para>
        /// <para> January 1st 1970 00:00:00 UTC).</para>
        /// </summary>
        public ulong timestamp { get; set; }

    }

    /// <summary> A definition (or update) of an entity in the transit feed.</summary>
    public partial class FeedEntity
    {
        /// <summary>
        /// <para> The ids are used only to provide incrementality support. The id should be</para>
        /// <para> unique within a FeedMessage. Consequent FeedMessages may contain</para>
        /// <para> FeedEntities with the same id. In case of a DIFFERENTIAL update the new</para>
        /// <para> FeedEntity with some id will replace the old FeedEntity with the same id</para>
        /// <para> (or delete it - see is_deleted below).</para>
        /// <para> The actual GTFS entities (e.g. stations, routes, trips) referenced by the</para>
        /// <para> feed must be specified by explicit selectors (see EntitySelector below for</para>
        /// <para> more info).</para>
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// <para> Whether this entity is to be deleted. Relevant only for incremental</para>
        /// <para> fetches.</para>
        /// </summary>
        public bool is_deleted { get; set; }

        /// <summary>
        /// <para> Data about the entity itself. Exactly one of the following fields must be</para>
        /// <para> present (unless the entity is being deleted).</para>
        /// </summary>
        public TransitRealtime.TripUpdate trip_update { get; set; }

        public TransitRealtime.VehiclePosition vehicle { get; set; }

        public TransitRealtime.Alert alert { get; set; }

    }

    /// <summary>
    /// <para> Entities used in the feed.</para>
    /// <para></para>
    /// <para> Realtime update of the progress of a vehicle along a trip.</para>
    /// <para> Depending on the value of ScheduleRelationship, a TripUpdate can specify:</para>
    /// <para> - A trip that proceeds along the schedule.</para>
    /// <para> - A trip that proceeds along a route but has no fixed schedule.</para>
    /// <para> - A trip that have been added or removed with regard to schedule.</para>
    /// <para></para>
    /// <para> The updates can be for future, predicted arrival/departure events, or for</para>
    /// <para> past events that already occurred.</para>
    /// <para> Normally, updates should get more precise and more certain (see</para>
    /// <para> uncertainty below) as the events gets closer to current time.</para>
    /// <para> Even if that is not possible, the information for past events should be</para>
    /// <para> precise and certain. In particular, if an update points to time in the past</para>
    /// <para> but its update's uncertainty is not 0, the client should conclude that the</para>
    /// <para> update is a (wrong) prediction and that the trip has not completed yet.</para>
    /// <para></para>
    /// <para> Note that the update can describe a trip that is already completed.</para>
    /// <para> To this end, it is enough to provide an update for the last stop of the trip.</para>
    /// <para> If the time of that is in the past, the client will conclude from that that</para>
    /// <para> the whole trip is in the past (it is possible, although inconsequential, to</para>
    /// <para> also provide updates for preceding stops).</para>
    /// <para> This option is most relevant for a trip that has completed ahead of schedule,</para>
    /// <para> but according to the schedule, the trip is still proceeding at the current</para>
    /// <para> time. Removing the updates for this trip could make the client assume</para>
    /// <para> that the trip is still proceeding.</para>
    /// <para> Note that the feed provider is allowed, but not required, to purge past</para>
    /// <para> updates - this is one case where this would be practically useful.</para>
    /// </summary>
    public partial class TripUpdate
    {
        /// <summary>
        /// <para> The Trip that this message applies to. There can be at most one</para>
        /// <para> TripUpdate entity for each actual trip instance.</para>
        /// <para> If there is none, that means there is no prediction information available.</para>
        /// <para> It does *not* mean that the trip is progressing according to schedule.</para>
        /// </summary>
        public TransitRealtime.TripDescriptor trip { get; set; }

        /// <summary> Additional information on the vehicle that is serving this trip.</summary>
        public TransitRealtime.VehicleDescriptor vehicle { get; set; }

        /// <summary>
        /// <para> Updates to StopTimes for the trip (both future, i.e., predictions, and in</para>
        /// <para> some cases, past ones, i.e., those that already happened).</para>
        /// <para> The updates must be sorted by stop_sequence, and apply for all the</para>
        /// <para> following stops of the trip up to the next specified one.</para>
        /// <para></para>
        /// <para> Example 1:</para>
        /// <para> For a trip with 20 stops, a StopTimeUpdate with arrival delay and departure</para>
        /// <para> delay of 0 for stop_sequence of the current stop means that the trip is</para>
        /// <para> exactly on time.</para>
        /// <para></para>
        /// <para> Example 2:</para>
        /// <para> For the same trip instance, 3 StopTimeUpdates are provided:</para>
        /// <para> - delay of 5 min for stop_sequence 3</para>
        /// <para> - delay of 1 min for stop_sequence 8</para>
        /// <para> - delay of unspecified duration for stop_sequence 10</para>
        /// <para> This will be interpreted as:</para>
        /// <para> - stop_sequences 3,4,5,6,7 have delay of 5 min.</para>
        /// <para> - stop_sequences 8,9 have delay of 1 min.</para>
        /// <para> - stop_sequences 10,... have unknown delay.</para>
        /// </summary>
        public List<TransitRealtime.TripUpdate.StopTimeUpdate> stop_time_update { get; set; }

        /// <summary>
        /// <para> Moment at which the vehicle's real-time progress was measured. In POSIX</para>
        /// <para> time (i.e., the number of seconds since January 1st 1970 00:00:00 UTC).</para>
        /// </summary>
        public ulong timestamp { get; set; }

        /// <summary>
        /// <para> Timing information for a single predicted event (either arrival or</para>
        /// <para> departure).</para>
        /// <para> Timing consists of delay and/or estimated time, and uncertainty.</para>
        /// <para> - delay should be used when the prediction is given relative to some</para>
        /// <para>   existing schedule in GTFS.</para>
        /// <para> - time should be given whether there is a predicted schedule or not. If</para>
        /// <para>   both time and delay are specified, time will take precedence</para>
        /// <para>   (although normally, time, if given for a scheduled trip, should be</para>
        /// <para>   equal to scheduled time in GTFS + delay).</para>
        /// <para></para>
        /// <para> uncertainty applies equally to both time and delay.</para>
        /// <para> The uncertainty roughly specifies the expected error in true delay (but</para>
        /// <para> note, we don't yet define its precise statistical meaning). It's possible</para>
        /// <para> for the uncertainty to be 0, for example for trains that are driven under</para>
        /// <para> computer timing control.</para>
        /// </summary>
        public partial class StopTimeEvent
        {
            /// <summary>
            /// <para> delay (in seconds) can be positive (meaning that the vehicle is late) or</para>
            /// <para> negative (meaning that the vehicle is ahead of schedule). delay of 0</para>
            /// <para> means that the vehicle is exactly on time.</para>
            /// </summary>
            public int delay { get; set; }

            /// <summary>
            /// <para> Event as absolute time.</para>
            /// <para> In Unix time (i.e., number of seconds since January 1st 1970 00:00:00</para>
            /// <para> UTC).</para>
            /// </summary>
            public long time { get; set; }

            /// <summary>
            /// <para> If uncertainty is omitted, it is interpreted as unknown.</para>
            /// <para> If the prediction is unknown or too uncertain, the delay (or time) field</para>
            /// <para> should be empty. In such case, the uncertainty field is ignored.</para>
            /// <para> To specify a completely certain prediction, set its uncertainty to 0.</para>
            /// </summary>
            public int uncertainty { get; set; }

        }

        /// <summary>
        /// <para> Realtime update for arrival and/or departure events for a given stop on a</para>
        /// <para> trip. Updates can be supplied for both past and future events.</para>
        /// <para> The producer is allowed, although not required, to drop past events.</para>
        /// </summary>
        public partial class StopTimeUpdate
        {
            public enum ScheduleRelationship
            {
                /// <summary>
                /// <para> The vehicle is proceeding in accordance with its static schedule of</para>
                /// <para> stops, although not necessarily according to the times of the schedule.</para>
                /// <para> At least one of arrival and departure must be provided. If the schedule</para>
                /// <para> for this stop contains both arrival and departure times then so must</para>
                /// <para> this update.</para>
                /// </summary>
                SCHEDULED = 0,
                /// <summary>
                /// <para> The stop is skipped, i.e., the vehicle will not stop at this stop.</para>
                /// <para> arrival and departure are optional.</para>
                /// </summary>
                SKIPPED = 1,
                /// <summary>
                /// <para> No data is given for this stop. The main intention for this value is to</para>
                /// <para> give the predictions only for part of a trip, i.e., if the last update</para>
                /// <para> for a trip has a NO_DATA specifier, then StopTimes for the rest of the</para>
                /// <para> stops in the trip are considered to be unspecified as well.</para>
                /// <para> Neither arrival nor departure should be supplied.</para>
                /// </summary>
                NO_DATA = 2,
            }

            /// <summary>
            /// <para> The update is linked to a specific stop either through stop_sequence or</para>
            /// <para> stop_id, so one of the fields below must necessarily be set.</para>
            /// <para> See the documentation in TripDescriptor for more information.</para>
            /// <para> Must be the same as in stop_times.txt in the corresponding GTFS feed.</para>
            /// </summary>
            public uint stop_sequence { get; set; }

            /// <summary> Must be the same as in stops.txt in the corresponding GTFS feed.</summary>
            public string stop_id { get; set; }

            public TransitRealtime.TripUpdate.StopTimeEvent arrival { get; set; }

            public TransitRealtime.TripUpdate.StopTimeEvent departure { get; set; }

            public TransitRealtime.TripUpdate.StopTimeUpdate.ScheduleRelationship schedule_relationship { get; set; }

        }

    }

    /// <summary> Realtime positioning information for a given vehicle.</summary>
    public partial class VehiclePosition
    {
        public enum VehicleStopStatus
        {
            /// <summary>
            /// <para> The vehicle is just about to arrive at the stop (on a stop</para>
            /// <para> display, the vehicle symbol typically flashes).</para>
            /// </summary>
            INCOMING_AT = 0,
            /// <summary> The vehicle is standing at the stop.</summary>
            STOPPED_AT = 1,
            /// <summary> The vehicle has departed and is in transit to the next stop.</summary>
            IN_TRANSIT_TO = 2,
        }

        public enum CongestionLevel
        {
            UNKNOWN_CONGESTION_LEVEL = 0,
            RUNNING_SMOOTHLY = 1,
            STOP_AND_GO = 2,
            CONGESTION = 3,
            SEVERE_CONGESTION = 4,
        }
        public enum OccupancyStatus
        {
            // The vehicle is considered empty by most measures, and has few or no
            // passengers onboard, but is still accepting passengers.
            EMPTY = 0,

            // The vehicle has a relatively large percentage of seats available.
            // What percentage of free seats out of the total seats available is to be
            // considered large enough to fall into this category is determined at the
            // discretion of the producer.
            MANY_SEATS_AVAILABLE = 1,

            // The vehicle has a relatively small percentage of seats available.
            // What percentage of free seats out of the total seats available is to be
            // considered small enough to fall into this category is determined at the
            // discretion of the feed producer.
            FEW_SEATS_AVAILABLE = 2,

            // The vehicle can currently accommodate only standing passengers.
            STANDING_ROOM_ONLY = 3,

            // The vehicle can currently accommodate only standing passengers
            // and has limited space for them.
            CRUSHED_STANDING_ROOM_ONLY = 4,

            // The vehicle is considered full by most measures, but may still be
            // allowing passengers to board.
            FULL = 5,

            // The vehicle is not accepting additional passengers.
            NOT_ACCEPTING_PASSENGERS = 6,
        }


        /// <summary>
        /// <para> The Trip that this vehicle is serving.</para>
        /// <para> Can be empty or partial if the vehicle can not be identified with a given</para>
        /// <para> trip instance.</para>
        /// </summary>
        public TransitRealtime.TripDescriptor trip { get; set; }

        /// <summary> Additional information on the vehicle that is serving this trip.</summary>
        public TransitRealtime.VehicleDescriptor vehicle { get; set; }

        /// <summary> Current position of this vehicle.</summary>
        public TransitRealtime.Position position { get; set; }

        /// <summary>
        /// <para> The stop sequence index of the current stop. The meaning of</para>
        /// <para> current_stop_sequence (i.e., the stop that it refers to) is determined by</para>
        /// <para> current_status.</para>
        /// <para> If current_status is missing IN_TRANSIT_TO is assumed.</para>
        /// </summary>
        public uint current_stop_sequence { get; set; }

        /// <summary>
        /// <para> Identifies the current stop. The value must be the same as in stops.txt in</para>
        /// <para> the corresponding GTFS feed.</para>
        /// </summary>
        public string stop_id { get; set; }

        //public string stop_id_1 { get; set; }

        /// <summary>
        /// <para> The exact status of the vehicle with respect to the current stop.</para>
        /// <para> Ignored if current_stop_sequence is missing.</para>
        /// </summary>
        public TransitRealtime.VehiclePosition.VehicleStopStatus current_status { get; set; }

        /// <summary>
        /// <para> Moment at which the vehicle's position was measured. In POSIX time</para>
        /// <para> (i.e., number of seconds since January 1st 1970 00:00:00 UTC).</para>
        /// </summary>
        public ulong timestamp { get; set; }

        /// <summary> People leaving their cars.</summary>
        public TransitRealtime.VehiclePosition.CongestionLevel congestion_level { get; set; }

        public TransitRealtime.VehiclePosition.OccupancyStatus occupancy_status { get; set; }
        //public string occupancy_status { get; set; }

         

    }

    /// <summary> An alert, indicating some sort of incident in the public transit network.</summary>
    public partial class Alert 
    {
        public enum Cause
        {
            UNKNOWN_CAUSE = 1,
            OTHER_CAUSE = 2,
            /// <summary> Not machine-representable.</summary>
            TECHNICAL_PROBLEM = 3,
            STRIKE = 4,
            /// <summary> Public transit agency employees stopped working.</summary>
            DEMONSTRATION = 5,
            /// <summary> People are blocking the streets.</summary>
            ACCIDENT = 6,
            HOLIDAY = 7,
            WEATHER = 8,
            MAINTENANCE = 9,
            CONSTRUCTION = 10,
            POLICE_ACTIVITY = 11,
            MEDICAL_EMERGENCY = 12,
        }

        public enum Effect
        {
            NO_SERVICE = 1,
            REDUCED_SERVICE = 2,
            /// <summary>
            /// <para> We don't care about INsignificant delays: they are hard to detect, have</para>
            /// <para> little impact on the user, and would clutter the results as they are too</para>
            /// <para> frequent.</para>
            /// </summary>
            SIGNIFICANT_DELAYS = 3,
            DETOUR = 4,
            ADDITIONAL_SERVICE = 5,
            MODIFIED_SERVICE = 6,
            OTHER_EFFECT = 7,
            UNKNOWN_EFFECT = 8,
            STOP_MOVED = 9,
        }

        /// <summary>
        /// <para> Time when the alert should be shown to the user. If missing, the</para>
        /// <para> alert will be shown as long as it appears in the feed.</para>
        /// <para> If multiple ranges are given, the alert will be shown during all of them.</para>
        /// </summary>
        public List<TransitRealtime.TimeRange> active_period { get; set; }

        /// <summary> Entities whose users we should notify of this alert.</summary>
        public List<TransitRealtime.EntitySelector> informed_entity { get; set; }

        public TransitRealtime.Alert.Cause cause { get; set; }

        public TransitRealtime.Alert.Effect effect { get; set; }

        /// <summary> The URL which provides additional information about the alert.</summary>
        public TransitRealtime.TranslatedString url { get; set; }

        /// <summary> Alert header. Contains a short summary of the alert text as plain-text.</summary>
        public TransitRealtime.TranslatedString header_text { get; set; }

        /// <summary>
        /// <para> Full description for the alert as plain-text. The information in the</para>
        /// <para> description should add to the information of the header.</para>
        /// </summary>
        public TransitRealtime.TranslatedString description_text { get; set; }

    }

    /// <summary>
    /// <para> Low level data structures used above.</para>
    /// <para></para>
    /// <para> A time interval. The interval is considered active at time 't' if 't' is</para>
    /// <para> greater than or equal to the start time and less than the end time.</para>
    /// </summary>
    public partial class TimeRange
    {
        /// <summary>
        /// <para> Start time, in POSIX time (i.e., number of seconds since January 1st 1970</para>
        /// <para> 00:00:00 UTC).</para>
        /// <para> If missing, the interval starts at minus infinity.</para>
        /// </summary>
        public ulong start { get; set; }

        /// <summary>
        /// <para> End time, in POSIX time (i.e., number of seconds since January 1st 1970</para>
        /// <para> 00:00:00 UTC).</para>
        /// <para> If missing, the interval ends at plus infinity.</para>
        /// </summary>
        public ulong end { get; set; }

    }

    /// <summary> A position.</summary>
    public partial class Position
    {
        /// <summary> Degrees North, in the WGS-84 coordinate system.</summary>
        public float latitude { get; set; }

        /// <summary> Degrees East, in the WGS-84 coordinate system.</summary>
        public float longitude { get; set; }

        /// <summary>
        /// <para> Bearing, in degrees, clockwise from North, i.e., 0 is North and 90 is East.</para>
        /// <para> This can be the compass bearing, or the direction towards the next stop</para>
        /// <para> or intermediate location.</para>
        /// <para> This should not be direction deduced from the sequence of previous</para>
        /// <para> positions, which can be computed from previous data.</para>
        /// </summary>
        public float bearing { get; set; }

        /// <summary> Odometer value, in meters.</summary>
        public double odometer { get; set; }

        /// <summary> Momentary speed measured by the vehicle, in meters per second.</summary>
        public float speed { get; set; }

        //public Int32 occupancy { get; set; }

    }

    /// <summary>
    /// <para> A descriptor that identifies an instance of a GTFS trip, or all instances of</para>
    /// <para> a trip along a route.</para>
    /// <para> - To specify a single trip instance, the trip_id (and if necessary,</para>
    /// <para>   start_time) is set. If route_id is also set, then it should be same as one</para>
    /// <para>   that the given trip corresponds to.</para>
    /// <para> - To specify all the trips along a given route, only the route_id should be</para>
    /// <para>   set. Note that if the trip_id is not known, then stop sequence ids in</para>
    /// <para>   TripUpdate are not sufficient, and stop_ids must be provided as well. In</para>
    /// <para>   addition, absolute arrival/departure times must be provided.</para>
    /// </summary>
    public partial class TripDescriptor
    {
        public enum ScheduleRelationship
        {
            /// <summary>
            /// <para> Trip that is running in accordance with its GTFS schedule, or is close</para>
            /// <para> enough to the scheduled trip to be associated with it.</para>
            /// </summary>
            SCHEDULED = 0,
            /// <summary>
            /// <para> An extra trip that was added in addition to a running schedule, for</para>
            /// <para> example, to replace a broken vehicle or to respond to sudden passenger</para>
            /// <para> load.</para>
            /// </summary>
            ADDED = 1,
            /// <summary>
            /// <para> A trip that is running with no schedule associated to it, for example, if</para>
            /// <para> there is no schedule at all.</para>
            /// </summary>
            UNSCHEDULED = 2,
            /// <summary> A trip that existed in the schedule but was removed.</summary>
            CANCELED = 3,
        }

        /// <summary>
        /// <para> The trip_id from the GTFS feed that this selector refers to.</para>
        /// <para> For non frequency expanded trips, this field is enough to uniquely identify</para>
        /// <para> the trip. For frequency expanded, start_time and start_date might also be</para>
        /// <para> necessary.</para>
        /// </summary>
        public string trip_id { get; set; }

        /// <summary> The route_id from the GTFS that this selector refers to.</summary>
        public string route_id { get; set; }

        /// <summary>
        /// <para> The scheduled start time of this trip instance.</para>
        /// <para> This field should be given only if the trip is frequency-expanded in the</para>
        /// <para> GTFS feed. The value must precisely correspond to start_time specified for</para>
        /// <para> the route in the GTFS feed plus some multiple of headway_secs.</para>
        /// <para> Format of the field is same as that of GTFS/frequencies.txt/start_time,</para>
        /// <para> e.g., 11:15:35 or 25:15:35.</para>
        /// </summary>
        public string start_time { get; set; }

        /// <summary>
        /// <para> The scheduled start date of this trip instance.</para>
        /// <para> Must be provided to disambiguate trips that are so late as to collide with</para>
        /// <para> a scheduled trip on a next day. For example, for a train that departs 8:00</para>
        /// <para> and 20:00 every day, and is 12 hours late, there would be two distinct</para>
        /// <para> trips on the same time.</para>
        /// <para> This field can be provided but is not mandatory for schedules in which such</para>
        /// <para> collisions are impossible - for example, a service running on hourly</para>
        /// <para> schedule where a vehicle that is one hour late is not considered to be</para>
        /// <para> related to schedule anymore.</para>
        /// <para> In YYYYMMDD format.</para>
        /// </summary>
        public string start_date { get; set; }

        public TransitRealtime.TripDescriptor.ScheduleRelationship schedule_relationship { get; set; }

    }

    /// <summary> Identification information for the vehicle performing the trip.</summary>
    public partial class VehicleDescriptor
    {
        /// <summary>
        /// <para> Internal system identification of the vehicle. Should be unique per</para>
        /// <para> vehicle, and can be used for tracking the vehicle as it proceeds through</para>
        /// <para> the system.</para>
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// <para> User visible label, i.e., something that must be shown to the passenger to</para>
        /// <para> help identify the correct vehicle.</para>
        /// </summary>
        public string label { get; set; }

        /// <summary> The license plate of the vehicle.</summary>
        public string license_plate { get; set; }

    }

    /// <summary> A selector for an entity in a GTFS feed.</summary>
    public partial class EntitySelector
    {
        /// <summary>
        /// <para> The values of the fields should correspond to the appropriate fields in the</para>
        /// <para> GTFS feed.</para>
        /// <para> At least one specifier must be given. If several are given, then the</para>
        /// <para> matching has to apply to all the given specifiers.</para>
        /// </summary>
        public string agency_id { get; set; }

        public string route_id { get; set; }

        /// <summary> corresponds to route_type in GTFS.</summary>
        public int route_type { get; set; }

        public TransitRealtime.TripDescriptor trip { get; set; }

        public string stop_id { get; set; }

    }

    /// <summary>
    /// <para> An internationalized message containing per-language versions of a snippet of</para>
    /// <para> text or a URL.</para>
    /// <para> One of the strings from a message will be picked up. The resolution proceeds</para>
    /// <para> as follows:</para>
    /// <para> 1. If the UI language matches the language code of a translation,</para>
    /// <para>    the first matching translation is picked.</para>
    /// <para> 2. If a default UI language (e.g., English) matches the language code of a</para>
    /// <para>    translation, the first matching translation is picked.</para>
    /// <para> 3. If some translation has an unspecified language code, that translation is</para>
    /// <para>    picked.</para>
    /// </summary>
    public partial class TranslatedString
    {
        /// <summary> At least one translation must be provided.</summary>
        public List<TransitRealtime.TranslatedString.Translation> translation { get; set; }

        public partial class Translation
        {
            /// <summary> A UTF-8 string containing the message.</summary>
            public string text { get; set; }

            /// <summary>
            /// <para> BCP-47 language code. Can be omitted if the language is unknown or if</para>
            /// <para> no i18n is done at all for the feed. At most one translation is</para>
            /// <para> allowed to have an unspecified language tag.</para>
            /// </summary>
            public string language { get; set; }

        }

    }

}
