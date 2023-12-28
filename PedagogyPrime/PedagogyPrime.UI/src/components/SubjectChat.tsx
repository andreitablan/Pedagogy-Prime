import { useContext, useEffect, useState } from 'react';
import axiosInstance from '../AxiosConfig';
import { Button, Form, FormControl, InputGroup, Offcanvas } from 'react-bootstrap';
import { UserContext } from '../App';
import { Message, MessageState } from '../models/Message';
import SendIcon from '@mui/icons-material/Send';
import RadioButtonUncheckedIcon from '@mui/icons-material/RadioButtonUnchecked';
import ChatIcon from '@mui/icons-material/Chat';
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';
import DoNotDisturbIcon from '@mui/icons-material/DoNotDisturb';
import { v4 as uuidv4 } from 'uuid';
import '../css/chat.scss'

const SubjectChat = ({ subjectId, subjectName }) => {
  const [show, setShow] = useState(false);
  const { user } = useContext(UserContext);
  const [messages, setMessages] = useState<Message[]>([]);
  const [formData, setFormData] = useState({ message: '' });

  useEffect(() => {
    getData();
  }, []);

  const getData = async () => {
    try {
      const response = await axiosInstance.get(`https://localhost:7136/api/v1.0/subjectMessages/${subjectId}`);
      const messages = response.data.resource.map(x => ({
        messageText: x.messageText,
        username: x.username,
        userId: x.userId,
        date: new Date(x.date),
        state: MessageState.Sent
      })).sort((a, b) => a.date - b.date);
      setMessages(messages);
    } catch (error) {
      console.log(error);
    }
  };

  const handleShow = () => setShow(true);
  const handleClose = () => setShow(false);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleMessageDate = (date: Date): string => {
    const currentDate = new Date();
    const inputDate = new Date(date);
    const isToday = currentDate.toDateString() === inputDate.toDateString();
    const formattedDate = isToday
      ? inputDate.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: false })
      : inputDate.toLocaleDateString();

    return formattedDate;
  };

  const handleSendMessage = async () => {
    const id = uuidv4();
    const newMessages = [
      ...messages,
      {
        id,
        messageText: formData.message,
        userId: user.id,
        date: new Date(),
        username: user.userName,
        state: MessageState.Sending
      }
    ];

    setMessages(newMessages);

    try {
      const result = await axiosInstance.post(`https://localhost:7136/api/v1.0/subjectMessages`, {
        text: formData.message,
        subjectId,
        id
      });

      const lastMessage = newMessages.find(x => x.id === result.data.resource);
      const updatedMessages = newMessages.filter(x => x.id !== result.data.resource);

      if (lastMessage !== undefined) {
        lastMessage.state = MessageState.Sent;
        setMessages([...updatedMessages, lastMessage]);
      }
    } catch (error) {
      console.log(error);
    }

    setFormData({
      message: ''
    });
  };

  return (
    <div className="chat-component">
      <Button className="btn btn-success" onClick={handleShow}>
        <ChatIcon />
      </Button>

      <Offcanvas show={show} onHide={handleClose}>
        <Offcanvas.Header closeButton>
          <Offcanvas.Title>{subjectName} Chat</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          <div className="content">
            {messages.map((message, index) => (
              <div key={index} className={`message-details ${user.id !== message.userId ? 'left' : 'right'}`}>
                {message.userId !== user.id && <div className='user'>{message.username}</div>}
                <div className="message">
                  <div className={`text ${user.id !== message.userId ? 'left' : 'right'}`} >
                    {message.messageText}
                  </div>
                  {message.userId === user.id && (
                    <div className={`${message.state === MessageState.Sent
                      ? 'sent' : message.state === MessageState.Sending
                      ? 'sending' : 'notSent'}`}>
                      {message.state === MessageState.Sending && <RadioButtonUncheckedIcon />}
                      {message.state === MessageState.Sent && <CheckCircleOutlineIcon />}
                      {message.state === MessageState.NotSent && <DoNotDisturbIcon />}
                    </div>
                  )}
                </div>
                <div className="date">
                  {handleMessageDate(message.date)}
                </div>
              </div>
            ))}
          </div>
          <InputGroup className="mb-3">
            <Form.Control
              type="text"
              placeholder="..."
              name="message"
              value={formData.message}
              onChange={handleInputChange}
            />
            <Button disabled={formData.message === ''} onClick={handleSendMessage}>
              <SendIcon />
            </Button>
          </InputGroup>
        </Offcanvas.Body>
      </Offcanvas>
    </div>
  );
}

export default SubjectChat;
