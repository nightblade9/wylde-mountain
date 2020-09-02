import { IDungeon } from './IDungeon'

// Matches Models.User in C#

export interface IUser
{
    id: any, // from mongoDB
    emailAddress: string,
    character:string,
    experiencePoints:number,
    currentHealthPoints:number,
    maxHealthPoints:number,
    currentSkillPoints:number,
    maxSkillPoints:number,
    level:number,

    // Not in model, assigned in CurrentUser.tsx
    dungeon: IDungeon
};
