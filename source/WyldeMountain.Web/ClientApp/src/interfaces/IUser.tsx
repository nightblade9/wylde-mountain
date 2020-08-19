import { IDungeon } from './IDungeon'

// Matches Models.User in C#

export interface IUser
{
    id: any, // from mongoDB
    emailAddress: string,
    dungeon: IDungeon
};
