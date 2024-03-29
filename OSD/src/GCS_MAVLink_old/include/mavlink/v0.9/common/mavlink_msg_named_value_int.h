// MESSAGE NAMED_VALUE_INT PACKING

#define MAVLINK_MSG_ID_NAMED_VALUE_INT 253

typedef struct __mavlink_named_value_int_t
{
    char name[10]; ///< Name of the debug variable
    int32_t value; ///< Signed integer value
} mavlink_named_value_int_t;

#define MAVLINK_MSG_ID_NAMED_VALUE_INT_LEN 14
#define MAVLINK_MSG_ID_253_LEN 14

#define MAVLINK_MSG_NAMED_VALUE_INT_FIELD_NAME_LEN 10

#define MAVLINK_MESSAGE_INFO_NAMED_VALUE_INT { \
	"NAMED_VALUE_INT", \
	2, \
	{  { "name", NULL, MAVLINK_TYPE_CHAR, 10, 0, offsetof(mavlink_named_value_int_t, name) }, \
         { "value", NULL, MAVLINK_TYPE_INT32_T, 0, 10, offsetof(mavlink_named_value_int_t, value) }, \
         } \
}


/**
 * @brief Pack a named_value_int message
 * @param system_id ID of this system
 * @param component_id ID of this component (e.g. 200 for IMU)
 * @param msg The MAVLink message to compress the data into
 *
 * @param name Name of the debug variable
 * @param value Signed integer value
 * @return length of the message in bytes (excluding serial stream start sign)
 */
static inline uint16_t mavlink_msg_named_value_int_pack(uint8_t system_id, uint8_t component_id, mavlink_message_t* msg,
        const char *name, int32_t value)
{
#if MAVLINK_NEED_BYTE_SWAP || !MAVLINK_ALIGNED_FIELDS
    char buf[14];
    _mav_put_int32_t(buf, 10, value);
    _mav_put_char_array(buf, 0, name, 10);
    memcpy(_MAV_PAYLOAD_NON_CONST(msg), buf, 14);
#else
    mavlink_named_value_int_t packet;
    packet.value = value;
    mav_array_memcpy(packet.name, name, sizeof(char)*10);
    memcpy(_MAV_PAYLOAD_NON_CONST(msg), &packet, 14);
#endif

    msg->msgid = MAVLINK_MSG_ID_NAMED_VALUE_INT;
    return mavlink_finalize_message(msg, system_id, component_id, 14);
}

/**
 * @brief Pack a named_value_int message on a channel
 * @param system_id ID of this system
 * @param component_id ID of this component (e.g. 200 for IMU)
 * @param chan The MAVLink channel this message was sent over
 * @param msg The MAVLink message to compress the data into
 * @param name Name of the debug variable
 * @param value Signed integer value
 * @return length of the message in bytes (excluding serial stream start sign)
 */
static inline uint16_t mavlink_msg_named_value_int_pack_chan(uint8_t system_id, uint8_t component_id, uint8_t chan,
        mavlink_message_t* msg,
        const char *name,int32_t value)
{
#if MAVLINK_NEED_BYTE_SWAP || !MAVLINK_ALIGNED_FIELDS
    char buf[14];
    _mav_put_int32_t(buf, 10, value);
    _mav_put_char_array(buf, 0, name, 10);
    memcpy(_MAV_PAYLOAD_NON_CONST(msg), buf, 14);
#else
    mavlink_named_value_int_t packet;
    packet.value = value;
    mav_array_memcpy(packet.name, name, sizeof(char)*10);
    memcpy(_MAV_PAYLOAD_NON_CONST(msg), &packet, 14);
#endif

    msg->msgid = MAVLINK_MSG_ID_NAMED_VALUE_INT;
    return mavlink_finalize_message_chan(msg, system_id, component_id, chan, 14);
}

/**
 * @brief Encode a named_value_int struct into a message
 *
 * @param system_id ID of this system
 * @param component_id ID of this component (e.g. 200 for IMU)
 * @param msg The MAVLink message to compress the data into
 * @param named_value_int C-struct to read the message contents from
 */
static inline uint16_t mavlink_msg_named_value_int_encode(uint8_t system_id, uint8_t component_id, mavlink_message_t* msg, const mavlink_named_value_int_t* named_value_int)
{
    return mavlink_msg_named_value_int_pack(system_id, component_id, msg, named_value_int->name, named_value_int->value);
}

/**
 * @brief Send a named_value_int message
 * @param chan MAVLink channel to send the message
 *
 * @param name Name of the debug variable
 * @param value Signed integer value
 */
#ifdef MAVLINK_USE_CONVENIENCE_FUNCTIONS

static inline void mavlink_msg_named_value_int_send(mavlink_channel_t chan, const char *name, int32_t value)
{
#if MAVLINK_NEED_BYTE_SWAP || !MAVLINK_ALIGNED_FIELDS
    char buf[14];
    _mav_put_int32_t(buf, 10, value);
    _mav_put_char_array(buf, 0, name, 10);
    _mav_finalize_message_chan_send(chan, MAVLINK_MSG_ID_NAMED_VALUE_INT, buf, 14);
#else
    mavlink_named_value_int_t packet;
    packet.value = value;
    mav_array_memcpy(packet.name, name, sizeof(char)*10);
    _mav_finalize_message_chan_send(chan, MAVLINK_MSG_ID_NAMED_VALUE_INT, (const char *)&packet, 14);
#endif
}

#endif

// MESSAGE NAMED_VALUE_INT UNPACKING


/**
 * @brief Get field name from named_value_int message
 *
 * @return Name of the debug variable
 */
static inline uint16_t mavlink_msg_named_value_int_get_name(const mavlink_message_t* msg, char *name)
{
    return _MAV_RETURN_char_array(msg, name, 10,  0);
}

/**
 * @brief Get field value from named_value_int message
 *
 * @return Signed integer value
 */
static inline int32_t mavlink_msg_named_value_int_get_value(const mavlink_message_t* msg)
{
    return _MAV_RETURN_int32_t(msg,  10);
}

/**
 * @brief Decode a named_value_int message into a struct
 *
 * @param msg The message to decode
 * @param named_value_int C-struct to decode the message contents into
 */
static inline void mavlink_msg_named_value_int_decode(const mavlink_message_t* msg, mavlink_named_value_int_t* named_value_int)
{
#if MAVLINK_NEED_BYTE_SWAP
    mavlink_msg_named_value_int_get_name(msg, named_value_int->name);
    named_value_int->value = mavlink_msg_named_value_int_get_value(msg);
#else
    memcpy(named_value_int, _MAV_PAYLOAD(msg), 14);
#endif
}
