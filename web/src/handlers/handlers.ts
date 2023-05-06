export const setTokenToLocalStorage = (key: string, value: string) : void => {
  window.localStorage.setItem(key, value);
};