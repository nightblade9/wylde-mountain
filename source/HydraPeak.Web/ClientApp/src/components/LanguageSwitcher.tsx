import React, { Component } from 'react';
import { globalSettings } from '../App';
import { light } from '@material-ui/core/styles/createPalette';

export default class LanguageSwitcher extends Component
{
    // Order matters because the callback gives us an index only
    supportedLanguages:Array<any> =
    [
        { "id": "en", "name": "English" },
        { "id": "ar", "name": "العربية" } 
    ]

    constructor(props:any)
    {
        super(props);
        this.changeLanguage = this.changeLanguage.bind(this);
    }

    changeLanguage(e:any):void
    {
        let index = e.target.selectedIndex;
        var targetLanguage = this.supportedLanguages[index].id;
        globalSettings.language = targetLanguage;
    }

    render()
    {
        return (
            <select onChange={this.changeLanguage}>
                {this.supportedLanguages.map(language => (
                    <option value={language.id}>{language.name}</option>
                ))}
            </select>
        );
    }
}