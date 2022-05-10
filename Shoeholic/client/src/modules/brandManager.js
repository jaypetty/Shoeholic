import { getToken } from "./authManager";
const _apiUrl = "/api/brand";

export const getAllBrands = () => {
    return getToken().then((token) =>
      fetch(`${_apiUrl}`, {
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