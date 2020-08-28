import { globalSettings } from '../App';
import { getCurrentUserAndDungeonAsync, isUserAuthenticated } from '../helpers/CurrentUser';
import { IDungeonEvent } from '../interfaces/IDungeon';
import { IUser } from '../interfaces/IUser';
import React, { useEffect, useState } from 'react';
import LocalizedStrings from 'react-localization';

export const CoreGame = () =>
{
  
  const [user, setUser] = useState<IUser | undefined>(undefined);
  const [fetchedUser, setFetchedUser] = useState(false);

  const languageStrings = new LocalizedStrings({
    "en": require('~/../../resources/components/CoreGame-en.json'),
    "ar": require('~/../../resources/components/CoreGame-ar.json')
  });
  languageStrings.setLanguage(globalSettings.language);

  useEffect(() => {
    const fetchUser = async () => {
      if (!isUserAuthenticated())
      {
        setFetchedUser(true); // don't keep fetching
      }
      else if (!fetchedUser)
      {
        setFetchedUser(true);
        var data = await getCurrentUserAndDungeonAsync();
        setUser(data);
      }
    }

    fetchUser();
  });

  if (user === undefined || user.dungeon === undefined)
  {
    return (
      <div>{languageStrings.loading}</div>
    );
  }
  else
  {
    const finalHtml = [];
    
    for (let i = 0; i < user.dungeon.currentFloor.events.length; i++)
    {
      const eventArray:IDungeonEvent[] = user.dungeon.currentFloor.events[i];
      const event:IDungeonEvent = eventArray[0]; // or null
      finalHtml.push(<li key={"choice" + i} >
        {event.eventType}: {event.data}
          <button onClick={e => console.log("You clicked fight #" + i)}>interact</button>
      </li>)
    }
    return (
      <React.Fragment>
        <div>
          <p>{languageStrings.formatString(languageStrings.floorIndicator, {"floorNumber": user.dungeon.currentFloor.floorNumber})}</p>
          <ol>
            {finalHtml}
          </ol>
        </div>
      </React.Fragment>
    );
  }
}