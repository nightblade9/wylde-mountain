import { isUserAuthenticated, getCurrentUserAndDungeonAsync } from "../../helpers/CurrentUser";
import { IDungeonEvent } from "../../interfaces/IDungeon";
import { IUser } from "../../interfaces/IUser";
import queryString from 'query-string';
import React, { useState, useEffect } from "react";

export const Battle = (props:any) =>
{
    const params:any = queryString.parse(props.location.search);
    const [user, setUser] = useState<IUser | undefined>(undefined);
    const [fetchedUser, setFetchedUser] = useState(false);
    const choice:number = parseInt(params.choice);
    const event:IDungeonEvent|undefined = user?.dungeon.currentFloor.events[choice][0];

    // TODO: replace all this by making the user available at the top-level, or in global, and pass it around
    useEffect(() => {
        const fetchUser = async () => {
            if (!isUserAuthenticated()) {
                setFetchedUser(true); // don't keep fetching
            }
            else if (!fetchedUser) {
                setFetchedUser(true);
                var data = await getCurrentUserAndDungeonAsync();
                setUser(data);
            }
        }

        fetchUser();
    });

    return (
        <div>
            <h1>{event?.eventType}</h1>
            <p>You face a(n) {event?.data}! It roars in fury!</p>
            <p><strong>You: </strong> {user?.currentHealthPoints}/{user?.maxHealthPoints}HP, {user?.currentSkillPoints}/{user?.currentSkillPoints}SP</p>
            {/* How do we get monster stats? API call? ... */}
        </div>
    );
}