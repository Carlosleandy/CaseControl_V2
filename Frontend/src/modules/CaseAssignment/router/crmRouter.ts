
import caseassignmentRepository from '../repository/caseassignmentRepository'

export default [    
    {
        path:'caseassignment',
        name:'caseassignment',
        component: () => import('../ui/caseassignment/CaseAssignment.vue'),
        meta: {
            i18n: ['caseassignment']
        },
        props: {
            caseassignmentRepository,
        }
    },
];
