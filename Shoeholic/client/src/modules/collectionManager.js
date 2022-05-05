import { getToken } from "./authManager";
const _apiUrl = "/api/collection";

export const getUserCollectionByUserId = () => {
    return getToken().then((token) =>
      fetch(`${_apiUrl}/UserCollectionByUser`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }).then((res) => {
        if (res.ok) {
          return res.json();
        } else {
          throw new Error("Error");
        }
      })
    );
  };