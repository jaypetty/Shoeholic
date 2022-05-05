import React, { useEffect, useState} from "react";
import { useParams } from "react-router-dom";
import { getUserCollectionByUserId } from "../../modules/collectionManager";
import Collection from "./Collection";

const MyCollections = () => {
    
    const [collections, setCollections] = useState([]);

    const getCollections = () => {
        getUserCollectionByUserId().then(collection => setCollections(collection));
    }

    useEffect(() => {
        getCollections();
    }, [])

    return (
        <div className="container">
            <div className="row justify-content-center">
                {collections.map((collection) => (
                    <Collection key={collection.id} collection={collection}  />
                ))}
            </div>
        </div>
    );
}

export default MyCollections;