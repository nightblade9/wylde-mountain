import { isUserAuthenticated, getCurrentUserAndDungeonAsync } from "../../helpers/CurrentUser";
import { IDungeonEvent } from "../../interfaces/IDungeon";
import { IUser } from "../../interfaces/IUser";
import queryString from 'query-string';
import React, { useState, useEffect } from "react";

export const Battle = (props:any) =>
{
    const PER_MESSAGE_TIME_SECONDS:number = 0.7;
    const params:any = queryString.parse(props.location.search);
    const [user, setUser] = useState<IUser | undefined>(undefined);
    const [battleOutcome, setBattleOutcome] = useState<string[]>([]);
    const [fetchedUser, setFetchedUser] = useState(false);
    const [fetchedBattle, setFetchedBattle] = useState(false);
    const [lastMessageIndex, setLastMessageIndex] = useState(-1);
    const [isDone, setIsDone] = useState(false);

    const choice:number = parseInt(params.choice);
    const event:IDungeonEvent|undefined = user?.dungeon.currentFloor.events[choice][0];

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

        const fetchOutcome = async() => {
            if (!fetchedBattle)
            {
                setFetchedBattle(true);
                const headers:Record<string, string> = {
                    "Bearer" : localStorage.getItem("userInfo") || ""
                  };
                
                  const response = await fetch('api/Battle?choice=' + choice, {
                    headers: headers
                  });
                  
                  if (response.ok)
                  {
                    const data = await response.json();
                    setBattleOutcome(data);
                  }
            }
        }

        fetchUser();
        fetchOutcome();
    });

    useEffect(() => {
        const timer = setInterval(() => {
            // Didn't load yet? Do nothing.
            if (battleOutcome == null || battleOutcome.length == 0)
            {
                return;
            }

            // Done showing everything? End timer.
            if (lastMessageIndex >= battleOutcome.length)
            {
                clearInterval(timer);
                setIsDone(true);
                return;
            }

            setLastMessageIndex(lastMessageIndex + 1);
        }, 1000 * PER_MESSAGE_TIME_SECONDS);

        return () => clearInterval(timer);
    })

    return (
        <div>
            <h1>{event?.eventType}</h1>
            <p>The battle begins!</p>
            <p><strong>You: </strong> {user?.currentHealthPoints}/{user?.maxHealthPoints}HP, {user?.currentSkillPoints}/{user?.currentSkillPoints}SP</p>
            <ul>
                {lastMessageIndex > 0 ?
                    battleOutcome.slice(0, lastMessageIndex).map(b => <li>{b}</li>) :
                    ""
                }
            </ul>
        </div>
    );
}