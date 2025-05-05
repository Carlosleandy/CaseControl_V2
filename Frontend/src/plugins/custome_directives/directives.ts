import claim from "./secuiry";

const useDirective = (app: any) => {
    app.directive('claim', claim);
}

export default useDirective;