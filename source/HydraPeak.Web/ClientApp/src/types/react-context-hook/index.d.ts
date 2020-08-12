declare module 'react-context-hook'
{
    export function withStore(clazz:object, state:any);
    export function useStore(getter:string);
    export function useStore(getter:string, setter:string);
}