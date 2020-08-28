import { globalSettings } from '../App';
import { getCurrentUserAndDungeonAsync, isUserAuthenticated } from '../helpers/CurrentUser';
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
    return (
      <div>
        <strong>{languageStrings.formatString(languageStrings.floorIndicator, {"floorNumber": user.dungeon.currentFloor.floorNumber})}</strong>
      </div>
    );
  }
}