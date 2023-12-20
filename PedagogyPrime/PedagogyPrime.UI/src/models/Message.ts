export enum MessageState{
    Sent,
    Sending,
    NotSent
}

export interface Message{
    id: string;
    userId: string;
    username: string;
    messageText: string;
    date: Date;
    state: MessageState
}