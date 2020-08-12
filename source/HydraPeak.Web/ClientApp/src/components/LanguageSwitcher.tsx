import React, { Component } from 'react';
import { useStore } from 'react-context-hook';

export default class LanguageSwitcher extends Component
{
    constructor(props:any)
    {
        super(props);
        this.changeLanguage = this.changeLanguage.bind(this);
    }

    changeLanguage(e:any):void
    {
        const [currentLanguage, setCurrentLanguage] = useStore("currentLanguage");
        let index = e.target.selectedIndex;
        console.log("Hi, idx=" + index);
        setCurrentLanguage("ar");
    }

    render()
    {
        const [currentLanguage] = useStore("currentLanguage");
        
        const supportedLanguages:Record<string, string> =
        {
            "en": "English" ,
            "ar": "العربية" 
        }

        const languagesList = Object.keys(supportedLanguages).map((l:string) => {
            return (
                <option value={l} {... l === currentLanguage ? "selected" : ""}>{supportedLanguages[l]}</option>
            )
        });

        return (
            <select onChange={this.changeLanguage}>
                {languagesList}
            </select>
        );
    }
}