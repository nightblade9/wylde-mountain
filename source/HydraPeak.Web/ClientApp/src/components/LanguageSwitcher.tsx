import React, { ChangeEvent } from 'react';
import { useCookies } from 'react-cookie';

import { COOKIE_NAME } from '../Constants';
import { globalSettings } from '../App';

const LanguageSwitcher = (props:any) =>
{
    // Order matters because the callback gives us an index only
    const supportedLanguages: Array<any> =
    [
        { "id": "en", "name": "English" },
        { "id": "ar", "name": "العربية" } 
    ]

    const [cookies, setCookies] = useCookies([COOKIE_NAME]);

    const ChangeLanguage = (event: ChangeEvent<HTMLSelectElement>) =>
    {
        let index = event.target.selectedIndex;
        var targetLanguage = supportedLanguages[index].id;
        globalSettings.language = targetLanguage;
        setCookies("language", targetLanguage, { path: "/" });
        console.log("Set cookie language to " + targetLanguage);
    }

    return (
        <select onChange={ChangeLanguage}>
            {supportedLanguages.map(language => (
                <option value={language.id}>{language.name}</option>
            ))}
        </select>
    );
}

export default LanguageSwitcher;