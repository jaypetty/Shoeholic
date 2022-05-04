import React from "react";
import { Card, CardBody, CardTitle, CardText} from "reactstrap";
import { Link } from "react-router-dom";

const Shoe = ({shoe}) => {
    return (
        <Card>
            <CardBody>
                <CardTitle tag="h1">
                    {shoe.name}
                </CardTitle>
                <CardText>
                  {shoe.title} 
                  <Link to={`/myshoes/details/${shoe.id}`}>Details</Link>
                </CardText>
            </CardBody>
        </Card>
    );
};

export default Shoe;