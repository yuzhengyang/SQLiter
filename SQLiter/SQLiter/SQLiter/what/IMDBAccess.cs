/*
 *  Copyright (c) 2014 The CCP project authors. All Rights Reserved.
 *
 *  Use of this source code is governed by a Beijing Speedtong Information Technology Co.,Ltd license
 *  that can be found in the LICENSE file in the root of the web site.
 *
 *                    http://www.yuntongxun.com
 *
 *  An additional intellectual property rights grant can be found
 *  in the file PATENTS.  All contributing project authors may
 *  be found in the AUTHORS file in the root of the source tree.
 */
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace NC_VideoConferenceSystem
{
    public enum MessageType : uint
    {
        Text = 0,
        File,
        Voice
    }

    public enum ConverType : uint
    {
        Message = 0,
        GroupNotice
    }

    public enum MessageState
    {
        Sending = 0,
        SendSuccess,
        SendFailed,
        Send_OtherReceived,
        Received
    };

    public enum GroupNoticeOperation
    {
        NeedAuth = 0,
        UnneedAuth,
        Access,
        Reject
    };

    public class IMConversation
    {
        public string conversationId;
        public string contact;
        public string date;
        public string content;
        public ConverType type;
    }

    public class IMMessageObj
    {
        public string msgid;
        public string sessionId;
        public string sender;
        public MessageType msgtype;
        public int isRead;
        public MessageState imState;
        public string dateCreated;
        public string curDate;
        public string userData;
        public string content;
        public string fileUrl;
        public string filePath;
        public double duration;
        public int isChunk;
    }

    public class IMGroupNotice
    {
        public string verifyMsg;
        public ECMessageNoticeCode msgType;
        public GroupNoticeOperation state;
        public int messageId;
        public string groupId;
        public string who;
        public string curDate;
        public int isRead;
    }

    public class IMDBAccess
    {
        static string dbPath = System.Windows.Forms.Application.StartupPath + @"\" + CCPCall.instance.configInfo.username + @"\immsg.db3";

        public static void CreateIMMessageTable()
        {
            //如果不存在改数据库文件，则创建该数据库文件 
            if (!System.IO.File.Exists(dbPath))
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\" + CCPCall.instance.configInfo.username);
                SQLiteDBHelper.CreateDB(dbPath);
            }
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            string sql = "create table if not exists im_message(msgId text primary key, sessionId text, msgType integer, sender text, isRead integer, imState integer, createDate text, curDate text, userData text, msgContent text, fileUrl text, filePath text, fileExt text, duration double,isChunk integer)";
            db.ExecuteNonQuery(sql, null);
        }

        public static void CreateGroupNoticeTabel()
        {
            //如果不存在改数据库文件，则创建该数据库文件 
            if (!System.IO.File.Exists(dbPath))
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\" + CCPCall.instance.configInfo.username);
                SQLiteDBHelper.CreateDB(dbPath);
            }
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            string sql = "create table if not exists im_groupnotice (id integer primary key, verifyMsg text, msgType integer, state integer, isRead integer, groupId text, who text, curDate text)";
            db.ExecuteNonQuery(sql, null);
        }

        public static void deleteAllMessage()
        {
            string sql = "delete from im_message";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            db.ExecuteNonQuery(sql, null);
        }

        public static void deleteAllGroupNotice()
        {
            string sql = "delete from im_groupnotice";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            db.ExecuteNonQuery(sql, null);
        }

        public static void deleteMessageBySession(string sessionId)
        {
            string sql = "delete from im_message where sessionId = \"" + sessionId + "\"";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            db.ExecuteNonQuery(sql, null);
        }

        public static void deleteMessageByMsgid(string msgid)
        {
            string sql = "delete from im_message where msgid = " + msgid;
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            db.ExecuteNonQuery(sql, null);
        }

        public static List<IMConversation> getIMList()
        {
            List<IMConversation> IMList = new List<IMConversation>();
            string getLastNotice = "select id, curDate, msgType, groupId, who from im_groupnotice order by curDate desc";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            using (SQLiteDataReader reader = db.ExecuteReader(getLastNotice, null))
            {
                while (reader.Read())
                {
                    IMConversation msg = new IMConversation();
                    msg.conversationId = "" + reader.GetInt32(0);
                    msg.contact = "系统通知";
                    msg.date = reader.GetValue(1).ToString();

                    ECMessageNoticeCode noticeType = (ECMessageNoticeCode)reader.GetInt32(2);
                    switch (noticeType)
                    {
                        case ECMessageNoticeCode.NTRequestJoinGroup:
                            msg.content = reader.GetValue(4).ToString() + @"申请加入群组[" + reader.GetValue(3).ToString() + "]";
                            break;
                        case ECMessageNoticeCode.NTDismissGroup:
                            msg.content = @"群组[" + reader.GetValue(3).ToString() + "]解散";
                            break;
                        case ECMessageNoticeCode.NTInviteJoinGroup:
                            msg.content = reader.GetValue(4).ToString() + @"邀请您加入群组[" + reader.GetValue(3).ToString() + "]";
                            break;
                        case ECMessageNoticeCode.NTMemberJoinedGroup:
                            msg.content = reader.GetValue(4).ToString() + @"加入群组[" + reader.GetValue(3).ToString() + "]";
                            break;
                        case ECMessageNoticeCode.NTQuitGroup:
                            msg.content = reader.GetValue(4).ToString() + @"退出群组[" + reader.GetValue(3).ToString() + "]";
                            break;
                        case ECMessageNoticeCode.NTRemoveGroupMember:
                            msg.content = (reader.GetValue(4).ToString() == CCPCall.instance.configInfo.username ? "您" : reader.GetValue(4).ToString()) + @"被一个群组[" + reader.GetValue(3).ToString() + "]踢出";
                            break;
                        case ECMessageNoticeCode.NTReplyRequestJoinGroup:
                            msg.content = @"申请加入群组[" + reader.GetValue(3).ToString() + "]的回复";
                            break;
                        default:
                            msg.content = "系统通知";
                            break;
                    }
                    msg.type = ConverType.GroupNotice;
                    IMList.Add(msg);
                }
            }

            string getMsgSql = "SELECT msgId, sessionId, curDate, msgType, msgContent, max(curDate) from im_message group  by sessionId order by curDate desc";
            using (SQLiteDataReader reader = db.ExecuteReader(getMsgSql, null))
            {
                while (reader.Read())
                {
                    IMConversation msg = new IMConversation();
                    msg.conversationId = reader.GetValue(0).ToString();
                    msg.contact = reader.GetValue(1).ToString();
                    msg.date = reader.GetValue(2).ToString();
                    msg.type = ConverType.Message;

                    MessageType msgtype = (MessageType)reader.GetInt32(3);
                    if (msgtype == MessageType.Text)
                    {
                        msg.content = reader.GetValue(4).ToString();
                    }
                    else if (msgtype == MessageType.File)
                    {
                        msg.content = "文件";
                    }
                    else if (msgtype == MessageType.Voice)
                    {
                        msg.content = "语音";
                    }
                    IMList.Add(msg);
                }
            }

            return IMList;
        }

        public static List<string> getAllFilePath()
        {
            List<string> fileList = new List<string>();
            string sql = "select filePath from im_message where filePath is not null";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    fileList.Add(reader.GetValue(0).ToString());
                }
            }

            return fileList;
        }

        public static List<string> getAllFilePathBySession(string sessionId)
        {
            List<string> fileList = new List<string>();
            string sql = "select filePath from im_message where filePath is not null and sessionId = " + sessionId;
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    fileList.Add(reader.GetValue(0).ToString());
                }
            }

            return fileList;
        }

        public static List<IMMessageObj> getMessageBySession(string sessionId)
        {
            List<IMMessageObj> immsgList = new List<IMMessageObj>();
            //            string sql = "select msgid,sessionId,msgType,sender,isRead,imState,createDate,curDate,userData,msgContent,fileUrl,filePath,fileExt,duration from im_message where sessionId = \"" + sessionId + "\" and (filePath is not null or msgType = 0)  order by curDate";
            string sql = "select msgid,sessionId,msgType,sender,isRead,imState,createDate,curDate,userData,msgContent,fileUrl,filePath,fileExt,duration from im_message where sessionId = \"" + sessionId + "\" order by curDate";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    IMMessageObj msg = new IMMessageObj();
                    int columnIndex = 0;
                    msg.msgid = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.sessionId = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.msgtype = (MessageType)reader.GetInt32(columnIndex); columnIndex++;
                    msg.sender = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.isRead = reader.GetInt32(columnIndex); columnIndex++;
                    msg.imState = (MessageState)reader.GetInt32(columnIndex); columnIndex++;
                    msg.dateCreated = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.curDate = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.userData = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.content = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.fileUrl = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.filePath = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.duration = reader.GetDouble(columnIndex);
                    immsgList.Add(msg);
                }
            }

            return immsgList;
        }

        public static List<IMMessageObj> getMessageOfFilePathisNull(string sessionId)
        {
            List<IMMessageObj> immsgList = new List<IMMessageObj>();
            string sql = "select msgid,sessionId,msgType,sender,isRead,imState,createDate,curDate,userData,msgContent,fileUrl,filePath,fileExt,duration,isChunk from im_message where filePath is null and (msgType = 1 or msgType = 2) order by curDate";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    IMMessageObj msg = new IMMessageObj();
                    int columnIndex = 0;
                    msg.msgid = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.sessionId = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.msgtype = (MessageType)reader.GetInt32(columnIndex); columnIndex++;
                    msg.sender = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.isRead = reader.GetInt32(columnIndex); columnIndex++;
                    msg.imState = (MessageState)reader.GetInt32(columnIndex); columnIndex++;
                    msg.dateCreated = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.curDate = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.userData = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.content = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.fileUrl = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.filePath = reader.GetValue(columnIndex).ToString(); columnIndex++;
                    reader.GetValue(columnIndex).ToString(); columnIndex++;
                    msg.duration = reader.GetDouble(columnIndex); columnIndex++;
                    msg.isChunk = reader.GetInt32(columnIndex);
                    immsgList.Add(msg);
                }
            }
            return immsgList;
        }

        public static bool isMessageExistByMsgid(string msgid)
        {
            bool isExist = false;
            string sql = "select count(*) from im_message where msgid = \"" + msgid + "\"";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    Int32 count = reader.GetInt32(0);
                    if (count > 0)
                        isExist = true;
                }
            }
            return isExist;
        }

        public static void insertIMMessage(IMMessageObj im)
        {
            string sql = "insert into im_message(msgid,sessionId,msgType,sender,isRead,imState,createDate,curDate,userData,msgContent,fileUrl,filePath,fileExt,duration,isChunk)"
                                      + "values (@msgid,@sessionId,@msgType,@sender,@isRead,@imState,@createDate,@curDate,@userData,@msgContent,@fileUrl,@filePath,@fileExt,@duration,@isChunk)";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);

            SQLiteParameter[] parameters = new SQLiteParameter[]{
                new SQLiteParameter("@msgid",im.msgid),
                new SQLiteParameter("@sessionId",im.sessionId),
                new SQLiteParameter("@msgType",im.msgtype),
                new SQLiteParameter("@sender",im.sender),
                new SQLiteParameter("@isRead",im.isRead),
                new SQLiteParameter("@imState",im.imState),
                new SQLiteParameter("@createDate",im.dateCreated),
                new SQLiteParameter("@curDate",im.curDate),
                new SQLiteParameter("@userData",im.userData),
                new SQLiteParameter("@msgContent",im.content),
                new SQLiteParameter("@fileUrl",im.fileUrl),
                new SQLiteParameter("@filePath",im.filePath),
                new SQLiteParameter("@fileExt",""),
                new SQLiteParameter("@duration",im.duration),
                new SQLiteParameter("@isChunk",im.isChunk)
            };

            db.ExecuteNonQuery(sql, parameters);
        }

        public static void insertNoticeMessage(IMGroupNotice notice)
        {
            string sql = "insert into im_groupnotice(verifyMsg,msgType,state,isRead,groupId,who,curDate) values (@verifyMsg,@msgType,@state,@isRead,@groupId,@who,@curDate)";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            SQLiteParameter[] parameters = new SQLiteParameter[] {
                new SQLiteParameter("@verifyMsg",notice.verifyMsg),
                new SQLiteParameter("@msgType",notice.msgType),
                new SQLiteParameter("@state",notice.state),
                new SQLiteParameter("@isRead",notice.isRead),
                new SQLiteParameter("@groupId",notice.groupId),
                new SQLiteParameter("@who",notice.who),
                new SQLiteParameter("@curDate",notice.curDate)
            };
            db.ExecuteNonQuery(sql, parameters);
        }

        public static IMGroupNotice getGroupNoticeById(int msgid)
        {
            string sql = "select id,verifyMsg,msgType,state,isRead,groupId,who,curDate from im_groupnotice where id = " + msgid;
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    IMGroupNotice notice = new IMGroupNotice();
                    notice.messageId = reader.GetInt32(0);
                    notice.verifyMsg = reader.GetValue(1).ToString();
                    notice.msgType = (ECMessageNoticeCode)reader.GetInt32(2);
                    notice.state = (GroupNoticeOperation)reader.GetInt32(3);
                    notice.isRead = reader.GetInt32(4);
                    notice.groupId = reader.GetValue(5).ToString();
                    notice.who = reader.GetValue(6).ToString();
                    notice.curDate = reader.GetValue(7).ToString();
                    return notice;
                }
            }
            return null;
        }

        public static List<IMGroupNotice> getAllGroupNotices()
        {
            List<IMGroupNotice> noticeList = new List<IMGroupNotice>();
            string sql = "select id,verifyMsg,msgType,state,isRead,groupId,who,curDate from im_groupnotice order by curDate";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    IMGroupNotice notice = new IMGroupNotice();
                    notice.messageId = reader.GetInt32(0);
                    notice.verifyMsg = reader.GetValue(1).ToString();
                    notice.msgType = (ECMessageNoticeCode)reader.GetInt32(2);
                    notice.state = (GroupNoticeOperation)reader.GetInt32(3);
                    notice.isRead = reader.GetInt32(4);
                    notice.groupId = reader.GetValue(5).ToString();
                    notice.who = reader.GetValue(6).ToString();
                    notice.curDate = reader.GetValue(7).ToString();
                    noticeList.Add(notice);
                }
            }
            return noticeList;
        }

        public static int getUnreadCountOfSessionId(string sessionId)
        {
            int count = 0;
            string sql = "select count(*) from im_message where isRead = 0 and sessionId = \"" + sessionId + "\"";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            return count;
        }

        public static void updateUnreadStateOfSessionId(string sessionId)
        {
            string sql = "update im_message set isRead = 1 where sessionId = \"" + sessionId + "\"";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            db.ExecuteNonQuery(sql, null);
        }

        public static void updateFile(string path, string msgid, double duration)
        {
            string sql = "update im_message set filePath = " + path + ",duration = " + duration + " where msgid = " + msgid;
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            db.ExecuteNonQuery(sql, null);
        }

        public static void updateMsgId(string newMsgId, string oldMsgId)
        {
            string sql = "update im_message set msgid = " + newMsgId + " where msgid = " + oldMsgId;
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            db.ExecuteNonQuery(sql, null);
        }

        public static void updateIMState(MessageState state, string msgId)
        {
            string sql = "update im_message set imState = " + (Int32)state + " where msgid = \"" + msgId + "\"";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            db.ExecuteNonQuery(sql, null);
        }

        public static void updateUnreadGroupNotice()
        {
            string sql = "update im_groupnotice set isRead=1 where isRead=0";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            db.ExecuteNonQuery(sql, null);
        }

        public static int getUnreadCountOfGroupNotice()
        {
            int count = 0;
            string sql = "select count(*) from im_groupnotice where isRead = 0";
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            using (SQLiteDataReader reader = db.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
            }
            return count;
        }

        public static void updateAllSendingToFailed()
        {
            string sql = "update im_message set imState=" + (Int32)MessageState.SendFailed + " where imState=" + (Int32)MessageState.Sending;
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            db.ExecuteNonQuery(sql, null);
        }

        public static void updateGroupNoticeState(GroupNoticeOperation state, string groupId, ECMessageNoticeCode msgType, string who)
        {
            string sql = "update im_groupnotice set state = " + (Int32)state + " where groupId=\"" + groupId + "\" and msgType=" + (Int32)msgType;
            if (who != null && who.Trim().Length > 0)
            {
                sql += " and who=\"" + who + "\"";
            }
            SQLiteDBHelper db = new SQLiteDBHelper(dbPath);
            db.ExecuteNonQuery(sql, null);
        }
    }
}
