export const setTokenToLocalStorage = (key: string, value: string) : void => {
  window.localStorage.setItem(key, value);
};

export const getTokenFromLocalStorage = (key: string): string | null => {
    const token = window.localStorage.getItem(key);
    if(!token) return null
  return token;
}