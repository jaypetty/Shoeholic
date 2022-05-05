import React from "react";
import { Card, CardBody, CardTitle, CardText} from "reactstrap";
import { Link } from "react-router-dom";

const Collection = ({collection}) => {
    return (
        <Card>
            <CardBody>
                <CardTitle tag="h1">
                    {collection.name}
                </CardTitle>
                <CardText> 
                  <Link to={`/mycollection/details/${collection.id}`}>View Collection</Link>
                </CardText>
            </CardBody>
        </Card>
    );
};

export default Collection;