// Matches Models.User in C#

export interface IDungeon
{
    id: any, // from mongoDB
    currentFloor: IFloor
};

export interface IFloor
{
    floorNumber: number,
    events: IDungeonEvent[][]
}

export interface IDungeonEvent
{
    eventType: string,
    data: string
}