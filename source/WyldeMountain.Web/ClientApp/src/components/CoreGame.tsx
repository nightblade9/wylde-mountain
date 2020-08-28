import { globalSettings } from '../App';
import { RequireAuthentication } from './Authentication/RequireAuthentication';
import { getCurrentUserAndDungeonAsync, isUserAuthenticated } from '../helpers/CurrentUser';
import { IUser } from '../interfaces/IUser';
import { useEffect, useState } from 'react';
import React from 'react';

export const CoreGame = () =>
{
  
  const [user, setUser] = useState<IUser | undefined>(undefined);
  const [fetchedUser, setFetchedUser] = useState(false);

  // const languageStrings = new LocalizedStrings({
  //   "en": require('~/../../resources/components/Home-en.json'),
  //   "ar": require('~/../../resources/components/Home-ar.json')
  // });
  // languageStrings.setLanguage(globalSettings.language);  

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
        console.log("I see you, eventz: " + JSON.stringify(data?.dungeon.currentFloor.events));
      }
    }

    fetchUser();
  });

  if (user === undefined || user.dungeon === undefined)
  {
    return (
      <div>loading</div>
    );
  }
  else
  {
    return (
      <div>
        <strong>You are on floor {user.dungeon.currentFloor.floorNumber}F</strong>
      </div>
    );
  }
}