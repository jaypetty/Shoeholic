import React, { useEffect, useState} from "react";
import Shoe from "./Shoe";
import { getAllShoes } from "../../modules/shoeManager";

const ShoeList = () => {
    const [shoes, setShoes] = useState([]);

    const getShoes = () => {
        getAllShoes().then(shoes => setShoes(shoes));
    };

    useEffect(() => {
        getShoes();
    }, []);

    return (
        <div className="container">
            <div className="row justify-content-center">
                {shoes.map((shoe) => (
                    <Shoe shoe={shoe} key={shoe.id} />
                ))}
            </div>
        </div>
    );
};

export default ShoeList;